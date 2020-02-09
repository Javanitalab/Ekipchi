using System.Threading.Tasks;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.DataAccess.Context;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

namespace Hastnama.Ekipchi.Business.Service.Class
{
    public class UserFilesService : Repository<EkipchiDbContext, UserFile>, IUserFilesService
    {
        public UserFilesService(EkipchiDbContext context) : base(context)
        {
        }

        public Task<PagedList<UserFile>> GetList(int pageNumber, int pageSize, string category)
        {
            throw new System.NotImplementedException();
        }

        public async Task<UserFile> GetUserFileAsync(string uniqueId)
        {
            return await GetAll().FirstOrDefaultAsync(x => x.UniqueId == uniqueId);
        }
    }
}