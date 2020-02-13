using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.Result;
using Newtonsoft.Json.Linq;

namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface IKavenegarApi
    {
        Task<Result<JObject>> Send(string sender, string receiver, string message);
    }
}