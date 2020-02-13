using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.Result;

namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface ISmsService
    {
        Task<Result> SendMessage(string mobile, string text);
    }
}