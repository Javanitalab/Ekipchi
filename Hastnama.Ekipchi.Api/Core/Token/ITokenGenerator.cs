using System.Threading.Tasks;
using Hastnama.Ekipchi.Data;
using Hastnama.Ekipchi.Data.Auth;
using Hastnama.Ekipchi.DataAccess.Entities;

namespace Hastnama.Ekipchi.Api.Core.Token
{
    public interface ITokenGenerator
    {
        Task<AuthenticateResult> Generate(User user);
    }
}