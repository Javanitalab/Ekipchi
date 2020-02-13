using System.Threading.Tasks;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Common.Result;
using Microsoft.AspNetCore.Mvc;

namespace Hastnama.Ekipchi.Business.Service.Class
{
    public class SmsService : ISmsService
    {
        private readonly KavenegarApi _kavenegarApi;

        public SmsService(KavenegarApi kavenegarApi)
        {
            _kavenegarApi = kavenegarApi;
        }

        public async Task<Result> SendMessage(string mobile, string text)
        {
            var sendResult = await _kavenegarApi.Send("1000596446", mobile, text);
            if (sendResult.Success)
                return Result.SuccessFull();
            return Result.Failed(new BadRequestObjectResult(new ApiMessage
                {Message = PersianErrorMessage.SendMessageFailed}));
        }
    }
}