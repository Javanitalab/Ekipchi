using System;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Api.Core.Extensions;
using Hastnama.Ekipchi.Api.Core.Token;
using Hastnama.Ekipchi.Data;
using Hastnama.Ekipchi.Data.Auth;
using Hastnama.Ekipchi.DataAccess.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Hastnama.Ekipchi.Api.Controllers.Auth
{
    [Route("/Auth")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly ITokenGenerator _tokenGenerator;

        public AuthController(ITokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
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
            var authToken = await _tokenGenerator.Generate(new User
                {Username = loginDto.Username, Email = loginDto.Username, Id = Guid.NewGuid()});


            return Ok(new TokenDto {AccessToken = authToken.AccessToken, RefreshToken = authToken.RefreshToken});
        }
    }
}