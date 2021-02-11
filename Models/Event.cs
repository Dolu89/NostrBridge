using System.Text.Json.Serialization;

namespace NostrBridge.Models
{
    public class Event
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("pubkey")]
        public string PubKey { get; set; }

        [JsonPropertyName("tags")]
        public string[][] Tags { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("sig")]
        public string Sig { get; set; }

        [JsonPropertyName("kind")]
        public int Kind { get; set; }

        [JsonPropertyName("created_at")]
        public long CreatedAt { get; set; }
    }
}
