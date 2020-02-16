using System;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;

namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface IUserMessageService : IRepository<UserMessage>
    {
        Task<PagedList<UserMessage>> GetSendMessageListAsync(PagingOptions pagingOptions, Guid senderUserId,
            string search);

        Task<PagedList<UserMessage>>
            GetReceiveMessageListAsync(PagingOptions pagingOptions, Guid userId, string search);

        Task<PagedList<UserMessage>> GetConversationListASync(int parentId, int objectPerPage, int pageNumber);

        Task<UserMessage> GetReceiveMessage(Guid userId, int messageId);

        Task<UserMessage> GetSendMessage(Guid userId, int messageId);

        Task<UserMessage> GetMessageAsync(int id);
    }
}