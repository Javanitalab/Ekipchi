using System;
using System.Threading.Tasks;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;

namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface IMessageService : IRepository<Message>
    {
        Task<Message> GetMessageAsync(Guid userId, int messageId);

        Task<bool> IsMessageExist(int parentId);
    }
}