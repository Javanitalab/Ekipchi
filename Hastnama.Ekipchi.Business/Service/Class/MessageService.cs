using System;
using System.Linq;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.DataAccess.Context;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

namespace Hastnama.Ekipchi.Business.Service.Class
{
    public class MessageService : Repository<EkipchiDbContext, Message>, IMessageService
    {
        public MessageService(EkipchiDbContext context) : base(context)
        {
        }

        public async Task<Message> GetMessageAsync(Guid userId, int messageId)
        {
            return await GetAll().Include(x => x.UserMessages).Include(x => x.ReplayToMessage).FirstOrDefaultAsync(x => x.Id == messageId && x.UserMessages.Any(m =>
                                                                                                                            (m.ReceiverHasDeleted == false && m.ReceiverUserId == userId) ||
                                                                                                                            (m.SenderHasDeleted == false && m.SenderUserId == userId)));
        }

        public async Task<bool> IsMessageExist(int parentId)
        {
            return await GetAll().AnyAsync(x => x.Id == parentId);
        }
    }
}