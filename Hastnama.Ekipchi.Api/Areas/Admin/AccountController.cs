using System;
using System.Linq;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Api.Core.Email;
using Hastnama.Ekipchi.Api.Core.Environment;
using Hastnama.Ekipchi.Api.Core.Extensions;
using Hastnama.Ekipchi.Api.Core.Token;
using Hastnama.Ekipchi.Business.Service;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Common.Util;
using Hastnama.Ekipchi.Data.Auth;
using Hastnama.Ekipchi.Data.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Hastnama.Ekipchi.Api.Areas.Admin
{
    [Route("[Controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailServices _emailServices;
        private readonly ISmsService _smsService;
        private readonly HostAddress _hostAddress;

        public AccountController(ITokenGenerator tokenGenerator, IUnitOfWork unitOfWork, IEmailServices emailServices,
            ISmsService smsService,
            IOptions<HostAddress> hostAddress)
        {
            _smsService = smsService;
            _emailServices = emailServices;
            _tokenGenerator = tokenGenerator;
            _unitOfWork = unitOfWork;
            _hostAddress = hostAddress.Value;
        }


        #region Auth

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

            //if token of user Is Already valid and exist in token pol use that token
            var userToken = await _unitOfWork.UserTokenService.GetUserTokenAsync(user.Data.Id);

            if (userToken != null)
                return Ok(new TokenDto {AccessToken = userToken.Token});


            var authToken = await _tokenGenerator.Generate(user.Data);

            if (!authToken.Success)
                return authToken.ApiResult;

            return Ok(new TokenDto
                {AccessToken = authToken.Data.AccessToken});
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
        /// logout user 
        /// </summary>
        /// <returns></returns>
        /// <response code="200">when user successfully log out </response>
        /// <response code="400">if invalid token send to system</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet]
        [Route("[Action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Logout()
        {
            var token = HttpContext.GetAuthorizationToken();

            var userToken = await _unitOfWork.UserTokenService.GetUserTokenAsync(token);

            if (userToken is null)
                return BadRequest(new ApiMessage {Message = ResponseMessage.TokenNotFound});

            userToken.IsUsed = true;

            _unitOfWork.UserTokenService.Edit(userToken);
            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }

        #endregion


        #region Password

        /// <summary>
        /// Change Password Need To User Authorize
        /// </summary>
        /// <param name="changePasswordDto"></param>
        /// <returns>return message to user</returns>
        /// <response code="200">if login successfully </response>
        /// <response code="400">If invalid Data Send Or Wrong Password </response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(ApiMessage), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPost]
        [Route("[Action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            if (!HttpContext.User.Claims.Any())
                return Unauthorized(new ApiMessage {Message = ResponseMessage.UnAuthorized});

            var userId = HttpContext.User?.GetUserId();

            var result = await _unitOfWork.UserService.ChangePassword(changePasswordDto, userId.Value);

            if (!result.Success)
                return result.ApiResult;

            var userTokens = await _unitOfWork.UserTokenService.GetAllByUser(userId.Value);

            if (!userTokens.Success || userTokens.Data == null)
                return BadRequest(new ApiMessage {Message = ResponseMessage.TokenNotFound});

            userTokens.Data.ToList().ForEach(userToken => userToken.IsUsed = true);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new ApiMessage {Message = "پسورد با موفقیت تقییر کرد"});
        }


        /// <summary>
        /// Forgot Password
        /// </summary>
        /// <param name="forgotPasswordDto"></param>
        /// <returns>return message to user</returns>
        /// <response code="200">if Forgot Password successfully </response>
        /// <response code="400">If invalid active or expired code send</response>
        /// <response code="404">If User not Found</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(ApiMessage), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPost]
        [Route("[Action]")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            #region Validation

            if (!ModelState.IsValid)
                return BadRequest(new ApiMessage {Message = ModelState.Values.ToList()[0].Errors[0].ErrorMessage});

            var user = await _unitOfWork.UserService.GetByEmail(forgotPasswordDto.Email);

            if (user is null)
                return Ok(new ApiMessage {Message = ResponseMessage.ForgotPasswordNotAccepted});

            #endregion Validation

            user.Data.ConfirmCode = new Random(999999).ToString();

            _unitOfWork.UserService.Edit(user.Data);
            await _unitOfWork.SaveChangesAsync();

            await _emailServices.SendMessage(forgotPasswordDto.Email, "فراموشی رمز عبور",
                $"{_hostAddress.ForgotPassword}{user.Data.ConfirmCode}");

            await _smsService.SendMessage(user.Data.Mobile, "کد تقییر رمز عبور شما : " + user.Data.ConfirmCode);

            return Ok(new ApiMessage {Message = ResponseMessage.ForgotPasswordAccepted});
        }

        /// <summary>
        /// Forgot Password
        /// </summary>
        /// <param name="resetPasswordDto"></param>
        /// <returns>return message to user</returns>
        /// <response code="200">if login successfully </response>
        /// <response code="400">If invalid data Send</response>
        /// <response code="404">If User not Found</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(ApiMessage), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPost]
        [Route("[Action]")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            #region Validation

            if (!ModelState.IsValid)
                return BadRequest(new ApiMessage {Message = ModelState.Values.ToList()[0].Errors[0].ErrorMessage});

            var user = await _unitOfWork.UserService.GetUserWithActivationCode(resetPasswordDto.ActiveCode);

            if (user is null)
                return BadRequest(new ApiMessage {Message = ResponseMessage.InvalidActiveCode});

            if (user.ExpiredVerificationCode < DateTime.Today)
                return BadRequest(
                    new ApiMessage {Message = ResponseMessage.InvalidActiveCode});

            #endregion Validation

            user.Password = StringUtil.HashPass(resetPasswordDto.Password);

            _unitOfWork.UserService.Edit(user);

            var userTokens = await _unitOfWork.UserTokenService.GetAllByUser(user.Id);

            if (!userTokens.Success || userTokens.Data == null)
                return BadRequest(new ApiMessage {Message = ResponseMessage.TokenNotFound});

            userTokens.Data.ToList().ForEach(userToken => userToken.IsUsed = true);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new ApiMessage {Message = ResponseMessage.PasswordSuccessfullyChanged});
        }

        #endregion


        #region Profile

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
                return Unauthorized(new ApiMessage {Message = ResponseMessage.UnAuthorized});

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
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPut("[Action]")]
        public async Task<IActionResult> Profile([FromBody] UpdateUserDto updateUserDto)
        {
            if (!HttpContext.User.Claims.Any())
                return Unauthorized(new ApiMessage {Message = ResponseMessage.UnAuthorized});

            var userId = HttpContext.User?.GetUserId();
            updateUserDto.Id = userId.Value;
            var result = await _unitOfWork.UserService.UpdateProfile(updateUserDto);
            if (!result.Success)
                return result.ApiResult;
            return Ok(result.Data);
        }

        /// <summary>
        /// User Roles 
        /// </summary>
        /// <returns>Ok</returns>
        /// <response code="200">if user roles successfully returned </response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPut("Role")]
        public async Task<IActionResult> UserRoles()
        {
            if (!HttpContext.User.Claims.Any())
                return Unauthorized(new ApiMessage {Message = ResponseMessage.UnAuthorized});

            var userId = HttpContext.User?.GetUserId();

            var result = await _unitOfWork.UserService.UserRoles(userId.Value);
            return result.ApiResult;
        }

        #endregion
    }
}