using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Data.Auth;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hastnama.Ekipchi.UnitTest.ControllerTest.Base
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class BaseControllerTest
    {
        protected const string _baseUrl = "http://localhost:5000";

        
        protected async Task<string> GetAdminAccessToken()
        {
            return await Login("mohammad.javan.t@gmail.com", "Mohammad12!@");
        }

        protected async Task<string> GetNormalUserAccessToken()
        {
            return await Login("kamranpr972@gmail.com", "Mohammad12!@");
        }

        private async Task<string> Login(string email, string password)
        {
            var httpClient = new HttpClient();

            var body = JsonConvert.SerializeObject(new LoginDto
                {Email = email, Password = password});
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            var login = await httpClient.PostAsync($"{_baseUrl}/Admin/Auth/Login", content);

            var responseString = await login.Content.ReadAsStringAsync();

            var token = JsonConvert.DeserializeObject<TokenDto>(responseString);

            return token.AccessToken;
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