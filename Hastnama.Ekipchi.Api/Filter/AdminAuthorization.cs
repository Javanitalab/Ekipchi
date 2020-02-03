using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Api.Core.Extensions;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Common.Enum;
using Hastnama.Ekipchi.Common.Message;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Hastnama.Ekipchi.Api.Filter
{
    public class AdminAuthorization : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private readonly IUnitOfWork _unitOfWork;


        public AdminAuthorization(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.GetAuthorizationToken();

            if (string.IsNullOrWhiteSpace(token))
            {
                context.Result =
                    new UnauthorizedObjectResult(new ApiMessage {Message = PersianErrorMessage.UnAuthorized});
                return;
            }

            var handler = new JwtSecurityTokenHandler();

            var tokenS = handler.ReadToken(token) as JwtSecurityToken;

            var id = new Guid(tokenS.Claims.First(claim => claim.Type == "Id").Value);

            var user = await _unitOfWork.UserService.Get(id);

            if (!user.Success)
            {
                context.Result = user.ApiResult;
                return;
            }

            if (user.Data.Role != Role.Admin)
                context.Result =
                    new UnauthorizedObjectResult(new ApiMessage {Message = PersianErrorMessage.UnAuthorized});
        }
    }
}