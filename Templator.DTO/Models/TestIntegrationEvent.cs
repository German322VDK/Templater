using Newtonsoft.Json;
using System.Collections.Generic;

namespace Templator.DTO.Models
{
    public class TestIntegrationEvent : IntegrationEvent
    {
        [JsonProperty]
        public Dictionary<string, string> Data { get; set; } = new Dictionary<string, string>();
    }
}
