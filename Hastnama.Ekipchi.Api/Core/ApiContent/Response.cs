using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hastnama.Ekipchi.Api.Core.ApiContent
{
    public static class Response
    {
        public static object ErrorMessage(string message)
        {
            var template = GetMessageTemplate(message);
            if (!string.IsNullOrEmpty(template))
                return new {message = template};
            else
                return new {message};
        }

        public static string GetMessageTemplate(string message)
        {
            const string resources = "Resources";
            const string fileName = "Hastnama.Ekipchi.ApiResponse.fa-IR.json";
            var path = Path.Combine(AppContext.BaseDirectory, $"{resources}", $"{fileName}");

            if (!File.Exists(path)) return "";

            var jsonData = File.ReadAllText(path);
            var localization = JsonConvert.DeserializeObject<JObject>(jsonData);

            var messageInfo = (localization["Data"] as JArray)?.FirstOrDefault(d => d["Key"].ToString() == message);
            return messageInfo?["Template"]?.ToString();
        }
    }
}