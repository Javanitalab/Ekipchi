using System.Threading.Tasks;
using Hastnama.Ekipchi.Api.Core.Token;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Data.Auth;
using Hastnama.Ekipchi.DataAccess.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Hastnama.Ekipchi.Api.Areas.Admin
{
    [Area("Admin")]
    [Route("[Area]/[Controller]")]
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
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(TokenDto), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [HttpPost]
        [Route("[Action]")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _unitOfWork.UserService.Login(loginDto);
            if (!user.Success)
                return user.ApiResult;

            var authToken = await _tokenGenerator.Generate(new User
                {Username = user.Data.Username, Email = user.Data.Username, Id = user.Data.Id});
            if (!authToken.Success)
                return authToken.ApiResult;
            return Ok(new TokenDto
                {AccessToken = authToken.Data.AccessToken, RefreshToken = authToken.Data.RefreshToken});
        }


        /// <summary>
        /// Register
        /// </summary>
        /// <param name="RegisterDto"></param>
        /// <returns>Access and refresh token.</returns>
        /// <response code="200">if login successfully </response>
        /// <response code="400">If validation failure.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(TokenDto), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
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