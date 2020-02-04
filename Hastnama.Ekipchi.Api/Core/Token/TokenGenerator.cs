using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data;
using Hastnama.Ekipchi.Data.Auth;
using Hastnama.Ekipchi.DataAccess.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Hastnama.Ekipchi.Api.Core.Token
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public TokenGenerator(IOptions<JwtSettings> jwtSettings, TokenValidationParameters tokenValidationParameters)
        {
            _tokenValidationParameters = tokenValidationParameters;

            _jwtSettings = jwtSettings.Value;
        }

        public async Task<Result<AuthenticateResult>> Generate(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Status.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("Username", user.Username),
                    new Claim("Roles", user.UserInRoles.Select(ur => $"{ur.RoleId}").Aggregate((a, b) => $"{a},{b}")),
                    new Claim("Id", user.Id.ToString())
                }),
                Expires = DateTime.Now.AddDays(14),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jwtSettings.ValidAudience,
                Issuer = _jwtSettings.ValidIssuer
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);


            return Result<AuthenticateResult>.SuccessFull(new AuthenticateResult
            {
                IsSuccess = true,
                AccessToken = tokenHandler.WriteToken(token),
                RefreshToken = token.Id
            });
        }
    }
}