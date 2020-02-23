using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Auth;
using Hastnama.Ekipchi.DataAccess.Entities;

namespace Hastnama.Ekipchi.Api.Core.Token
{
    public interface ITokenGenerator
    {
        Task<Result<AuthenticateResult>> Generate(User user);
    }
}