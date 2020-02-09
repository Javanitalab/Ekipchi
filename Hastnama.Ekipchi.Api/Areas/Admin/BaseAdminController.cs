using System;
using Hastnama.Ekipchi.Api.Core.Extensions;
using Hastnama.Ekipchi.Api.Filter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Hastnama.Ekipchi.Api.Areas.Admin
{
    [Area("Admin")]
    [Route("[Area]/[Controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ServiceFilter(typeof(AdminAuthorization))]
    public class BaseAdminController : ControllerBase
    {
        public Guid UserId => HttpContext.User.GetUserId();

    }
}