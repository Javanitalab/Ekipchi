using System.Threading.Tasks;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Common.Sms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Hastnama.Ekipchi.Business.Service.Class
{
    public class SmsService : ISmsService
    {
        private readonly KavenegarApi _kavenegarApi;
        private readonly KavenegarSetting _kavenegarSetting;

        public SmsService(KavenegarApi kavenegarApi, IOptions<KavenegarSetting> kavenegarSetting)
        {
            _kavenegarSetting = kavenegarSetting.Value;
            _kavenegarApi = kavenegarApi;
        }

        public async Task<Result> SendMessage(string mobile, string text)
        {
            var sendResult = await _kavenegarApi.Send(_kavenegarSetting.Sender, mobile, text);
            if (sendResult.Success)
                return Result.SuccessFull();
            return Result.Failed(new BadRequestObjectResult(new ApiMessage
                {Message = ResponseMessage.SendMessageFailed}));
        }
    }
}