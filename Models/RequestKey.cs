using System.Collections.Generic;

namespace NostrBridge.Models
{
    public class RequestKey
    {
        public IEnumerable<string> Relays { get; set; }
        public string PubKey { get; set; }
        public short Limit { get; set; } = 5;
        public int[] Kinds { get; set; } = new int[] { 1 };
    }
}
