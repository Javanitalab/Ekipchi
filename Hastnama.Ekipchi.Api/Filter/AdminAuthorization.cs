using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Api.Core.Extensions;
using Hastnama.Ekipchi.Business.Service;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Common.Statistics;
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

            var userToken = await _unitOfWork.UserTokenService.GetUserTokenAsync(id);
            if (userToken == null || userToken.IsUsed || userToken.ExpiredDate < DateTime.Now)
                context.Result =
                    new UnauthorizedObjectResult(new ApiMessage {Message = PersianErrorMessage.UnAuthorized});


            var roles = await _unitOfWork.UserService.UserRoles(id);

            if (!roles.Success)
            {
                context.Result = roles.ApiResult;
                return;
            }

            if (!roles.Data.Any() || !roles.Data.Any(r => r.Name == StaticPermissions.Admin))
                context.Result =
                    new UnauthorizedObjectResult(new ApiMessage {Message = PersianErrorMessage.UnAuthorized});
        }
    }
}