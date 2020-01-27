using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hastnama.Ekipchi.UnitTest.ControllerTest.Base
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class BaseControllerTest
    {
        protected const string _baseUrl = " http://localhost:5000";

  
        protected string GetMessageInfo(string message, string languageCode)
        {
            var path =
                Directory.GetCurrentDirectory().Split("\\").Reverse().Skip(4).Reverse()
                    .Aggregate((a, b) => a + "/" + b) + "/Hastnama.GuitarIranShop.Api/Resources/" +
                $"{MessageInfoPathForSpecificLanguage(languageCode)}";


            if (!File.Exists(path)) return null;

            var jsonData = File.ReadAllText(path);
            var localization = JsonConvert.DeserializeObject<JObject>(jsonData);

            var messageInfo = (localization["Data"] as JArray)?.FirstOrDefault(d => d["Key"].ToString() == message);
            return messageInfo?["Template"].ToString();
        }

        private string MessageInfoPathForSpecificLanguage(string languageCode)
        {
            if (languageCode == "fa-IR")
                return "Techravity.Shop.fa-IR.json";
            return "Techravity.Shop.en-US.json";
        }

        protected string DetectLanguage(string message)
        {
            if (Regex.IsMatch(message, @"\p{IsArabic}"))
                return "fa-IR";
            return "en-US";
        }

        protected static async Task<string> ExtractMessage(HttpResponseMessage response)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<JObject>(responseBody);
            var message = Convert.ToString(responseObject["message"]);
            return message;
        }

    }
}