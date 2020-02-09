using System.Threading.Tasks;
using Hastnama.Ekipchi.Api.Core.Token;
using Hastnama.Ekipchi.Business.Service;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Auth;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Hastnama.Ekipchi.Api.Areas.Admin
{
    [Route("[Controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IUnitOfWork _unitOfWork;

        public AuthController(ITokenGenerator tokenGenerator, IUnitOfWork unitOfWork)
        {
            _tokenGenerator = tokenGenerator;
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// login to
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns>Access and refresh token.</returns>
        /// <response code="200">if login successfully </response>
        /// <response code="400">If validation failure.</response>
        /// <response code="404">If User Not Fround.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(TokenDto), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPost]
        [Route("[Action]")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _unitOfWork.UserService.Login(loginDto);

            if (!user.Success)
                return user.ApiResult;

            var authToken = await _tokenGenerator.Generate(user.Data);

            if (!authToken.Success)
                return authToken.ApiResult;

            return Ok(new TokenDto
                {AccessToken = authToken.Data.AccessToken, RefreshToken = authToken.Data.RefreshToken});
        }


        /// <summary>
        /// Register
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns>Access and refresh token.</returns>
        /// <response code="200">if login successfully </response>
        /// <response code="400">If validation failure.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(TokenDto), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPost]
        [Route("[Action]")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var user = await _unitOfWork.UserService.Register(registerDto);

            if (!user.Success)
                return user.ApiResult;

            return Ok();
        }
    }
}