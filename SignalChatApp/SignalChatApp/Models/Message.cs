using Newtonsoft.Json;
using System;

namespace SignalChatApp.Models
{
    public class Message
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonIgnore]
        public DateTime TimeReceived { get; set; }
    }
}
