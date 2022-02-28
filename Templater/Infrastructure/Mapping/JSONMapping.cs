using Newtonsoft.Json;
using System.Collections.Generic;
using Templator.DTO.Models;

namespace Templater.Infrastructure.Mapping
{
    public static class JSONMapping
    {
        public static string ToJSONKeys(this ICollection<string> obj) =>
            JsonConvert.SerializeObject(obj);

        public static ICollection<string> FromJSONKeys(this string obj) =>
            JsonConvert.DeserializeObject<ICollection<string>>(obj);

        public static string ToJSONKeyValue(this Dictionary<string, string> obj) =>
            JsonConvert.SerializeObject(obj);

        public static Dictionary<string, string> FromJSONKeyValue(this string obj) =>
            JsonConvert.DeserializeObject<Dictionary<string, string>>(obj);

        public static DataIntegrationEvent FromData(this string obj) =>
            JsonConvert.DeserializeObject<DataIntegrationEvent>(obj);
    }
}
