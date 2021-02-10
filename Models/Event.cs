using Newtonsoft.Json;

namespace NostrBridge.Models
{
    public class Event
    {
        public string Id { get; set; }
        public string PubKey { get; set; }
        public string[][] Tags { get; set; }
        public string Content { get; set; }
        public string Sig { get; set; }
        public int Kind { get; set; }
        [JsonProperty("created_at")]
        public long CreatedAd { get; set; }
    }
}
