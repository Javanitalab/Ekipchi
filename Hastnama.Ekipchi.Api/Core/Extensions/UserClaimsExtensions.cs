using System;
using System.Linq;
using System.Security.Claims;

namespace Hastnama.Ekipchi.Api.Core.Extensions
{
    public static class UserClaimsExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
            => new Guid(claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "Id")?.Value);

        public static int GetRoleId(this ClaimsPrincipal claimsPrincipal)
            => int.Parse(claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "RoleId")?.Value);

        public static string GetEmail(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "Email")?.Value;


    }
}