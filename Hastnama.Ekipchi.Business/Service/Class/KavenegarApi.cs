using System.Net.Http;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Common.Result;
using Newtonsoft.Json.Linq;

namespace Hastnama.Ekipchi.Business.Service.Class
{
    public class KavenegarApi : IKavenegarApi
    {
        public string ApiKey { get; } =
            "7652785137424E46356A35445A4A6B6A6946654C747376476D52374B4F7A396F65573157496D5A45316B633D";

        public async Task<Result<JObject>> Send(string sender, string receiver, string message)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(
                $"https://api.kavenegar.com/v1/{ApiKey}/sms/send?sender={sender}&receptor={receiver}&message={message}");
            var stringResponse = await response.Content.ReadAsStringAsync();

            return Result<JObject>.SuccessFull(new JObject());
        }
    }
}