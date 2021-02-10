using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NostrBridge.Models
{
    public class RequestKey
    {
        [Required]
        public IEnumerable<string> Relays { get; set; }
        [Required]
        public string PubKey { get; set; }
        public short Limit { get; set; } = 5;
        public int[] Kinds { get; set; } = new int[] { 1 };
    }
}
