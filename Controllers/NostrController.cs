using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NostrBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Websocket.Client;

namespace NostrBridge.Controllers
{
    [ApiController]
    [Route("req-key")]
    public class NostrController : ControllerBase
    {
        [HttpPost]
        public IActionResult Subscribe([FromBody] RequestKey request)
        {
            var results = new List<Tuple<Event, string>>();

            foreach (var relay in request.Relays)
            {
                var exitEvent = new ManualResetEvent(false);
                var url = new Uri(relay);

                using (var client = new WebsocketClient(url))
                {
                    client.ReconnectTimeout = TimeSpan.FromSeconds(30);
                    client.ReconnectionHappened.Subscribe(info =>
                        client.MessageReceived.Where(x => Deserialize(x.Text).Kind == 1).Subscribe(msg =>
                        {
                            results.Add(new Tuple<Event, string>(Deserialize(msg.Text), msg.Text));
                            exitEvent.Set();
                        })
                    );
                    client.Start();

                    Task.Run(() =>
                    {
                        var obj = new { key = request.PubKey };
                        string objJson = JsonConvert.SerializeObject(obj);
                        client.Send($"req-key:{objJson}");
                    });

                    // Wait 2 secs for messages. Exit after 2 secs in any case.
                    exitEvent.WaitOne(TimeSpan.FromSeconds(2));
                }
            }
            var result = results.OrderByDescending(t => t.Item1.CreatedAd).Select(t => t.Item2).First();
            return Ok(JsonConvert.SerializeObject(result));
        }

        private Event Deserialize(string message)
        {
            var resultArray = JsonConvert.DeserializeObject<object[]>(message);
            var resultStringFromRelay = resultArray[0].ToString();
            return JsonConvert.DeserializeObject<Event>(resultStringFromRelay);
        }
    }
}
