using Newtonsoft.Json;
using System.Collections.Generic;

namespace Templator.DTO.Models
{
    public class DataIntegrationEvent : IntegrationEvent
    {
        [JsonProperty]
        public string FileName { get; set; }

        [JsonProperty]
        public string TemplateId { get; set; }

        [JsonProperty]
        public Dictionary<string, string> Data { get; set; } = new Dictionary<string, string>();
    }
}
