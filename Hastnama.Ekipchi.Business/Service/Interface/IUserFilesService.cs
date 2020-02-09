using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;

namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface IUserFilesService : IRepository<UserFile>
    {
        Task<PagedList<UserFile>> GetList(int pageNumber, int pageSize, string category);

        Task<UserFile> GetUserFileAsync(string uniqueId);
    }
}