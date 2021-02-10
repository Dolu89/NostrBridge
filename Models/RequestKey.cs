using System.Collections.Generic;

namespace NostrBridge.Models
{
    public class RequestKey
    {
        public IEnumerable<string> Relays { get; set; }
        public string PubKey { get; set; }
    }
}
