using System;
using System.Linq;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Api.Core.Extensions;
using Hastnama.Ekipchi.Api.Core.Token;
using Hastnama.Ekipchi.Business.Service;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Auth;
using Hastnama.Ekipchi.Data.User;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Hastnama.Ekipchi.Api.Areas.Admin
{
    [Route("[Controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(ITokenGenerator tokenGenerator, IUnitOfWork unitOfWork)
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

        /// <summary>
        /// User Profile
        /// </summary>
        /// <returns>Access and refresh token.</returns>
        /// <response code="200">if login successfully </response>
        /// <response code="400">If validation failure.</response>
        /// <response code="401">If user not Logined.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 401)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet]
        [Route("[Action]")]
        public async Task<IActionResult> Profile()
        {
            if (!HttpContext.User.Claims.Any())
                return Unauthorized(new ApiMessage {Message = PersianErrorMessage.UnAuthorized});

            var userId = HttpContext.User?.GetUserId();
            var user = await _unitOfWork.UserService.Get(userId.Value);
            return user.ApiResult;
        }
        
        
        /// <summary>
        /// Update Profile 
        /// </summary>
        /// <param name="updateUserDto"></param>
        /// <returns>NoContent</returns>
        /// <response code="204">if Update successfully </response>
        /// <response code="400">If validation failure.</response>
        /// <response code="404">If entity not found.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPut("[Action]")]
        public async Task<IActionResult> Profile(Guid? id, [FromBody] UpdateUserDto updateUserDto)
        {
            if (!HttpContext.User.Claims.Any())
                return Unauthorized(new ApiMessage {Message = PersianErrorMessage.UnAuthorized});

            var userId = HttpContext.User?.GetUserId();
            updateUserDto.Id = userId.Value;
            var result = await _unitOfWork.UserService.UpdateProfile(updateUserDto);
            if (!result.Success)
                return result.ApiResult;
            return NoContent();
        }

        
    }
}