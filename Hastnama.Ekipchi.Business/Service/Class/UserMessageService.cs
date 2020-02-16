using System;
using System.Linq;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.DataAccess.Context;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

namespace Hastnama.Ekipchi.Business.Service.Class
{
    public class UserMessageService : Repository<EkipchiDbContext, UserMessage>, IUserMessageService
    {
        public UserMessageService(EkipchiDbContext context) : base(context)
        {
        }

        public async Task<PagedList<UserMessage>> GetSendMessageListAsync(PagingOptions pagingOptions,
            Guid senderUserId, string search)
        {
            IQueryable<UserMessage> userMessages = GetAll()
                .Include(x => x.ReceiverUser).Include(x => x.SenderUser).Include(x => x.Message)
                .Where(x => x.SenderHasDeleted == false && x.SenderUserId == senderUserId &&
                            x.Message.ParentId == null);

            if (!string.IsNullOrWhiteSpace(search))
                userMessages = userMessages.Where(x => x.Message.Title.Contains(search)
                                                       || x.Message.Body.Contains(search) ||
                                                       x.SenderUser.Email.Contains(search.ToLower()) ||
                                                       x.ReceiverUser.Email.Contains(search.ToLower()));

            switch (pagingOptions.OrderBy.ToLower())
            {
                case "senddate":
                    userMessages = pagingOptions.Desc
                        ? userMessages.OrderByDescending(x => x.SendDate)
                        : userMessages.OrderBy(x => x.SendDate);
                    break;
                default:
                    userMessages = userMessages.OrderByDescending(x => x.SendDate);
                    break;
            }

            return await GetPagedAsync(pagingOptions.Page, pagingOptions.Limit, userMessages);
        }

        public async Task<PagedList<UserMessage>> GetSendMessageListAsync(Guid senderUserId, int objectPerPage,
            int pageNumber)
        {
            return await GetPagedAsync(pageNumber, objectPerPage, GetAll()
                .Include(x => x.ReceiverUser).Include(x => x.SenderUser).Include(x => x.Message)
                .Where(x => x.SenderHasDeleted == false && x.SenderUserId == senderUserId && x.Message.ParentId == null)
                .OrderByDescending(x => x.Message.CreateDate));
        }

        public async Task<PagedList<UserMessage>> GetSendMessageListAsync(Guid senderUserId, string search,
            int objectPerPage, int pageNumber)
        {
            return await GetPagedAsync(pageNumber, objectPerPage, GetAll().Include(x => x.ReceiverUser)
                .Include(x => x.SenderUser)
                .Include(x => x.Message).Where(x => x.SenderHasDeleted == false
                                                    && x.SenderUserId == senderUserId
                                                    && x.Message.ParentId == null && (x.Message.Title.Contains(search)
                                                                                      || x.Message.Body.Contains(search)
                                                    )));
        }

        public async Task<PagedList<UserMessage>> GetReceiveMessageListAsync(PagingOptions pagingOptions, Guid userId,
            string search)
        {
            IQueryable<UserMessage> userMessages = GetAll().Include(x => x.ReceiverUser).Include(x => x.SenderUser)
                .Include(x => x.Message).Where(x =>
                    x.ReceiverHasDeleted == false && x.ReceiverUserId == userId && x.Message.ParentId == null);

            if (!string.IsNullOrWhiteSpace(search))
            {
                userMessages = userMessages.Where(x =>
                    x.Message.Title.Contains(search) || x.Message.Body.Contains(search));
            }

            switch (pagingOptions.OrderBy.ToLower())
            {
                case "senddate":
                    userMessages = pagingOptions.Desc
                        ? userMessages.OrderByDescending(x => x.SendDate)
                        : userMessages.OrderBy(x => x.SendDate);
                    break;
                default:
                    userMessages = userMessages.OrderByDescending(x => x.SendDate);
                    break;
            }

            return await GetPagedAsync(pagingOptions.Page, pagingOptions.Limit, userMessages);
        }

        public async Task<PagedList<UserMessage>> GetReceiveMessageListAsync(Guid userId, int objectPerPage,
            int pageNumber)
        {
            return await GetPagedAsync(pageNumber, objectPerPage, GetAll().Include(x => x.ReceiverUser)
                .Include(x => x.SenderUser)
                .Include(x => x.Message).Where(x =>
                    x.ReceiverHasDeleted == false && x.ReceiverUserId == userId && x.Message.ParentId == null));
        }

        public async Task<PagedList<UserMessage>> GetReceiveMessageListAsync(Guid userId, string search,
            int objectPerPage, int pageNumber)
        {
            return await GetPagedAsync(pageNumber, objectPerPage, GetAll().Include(x => x.ReceiverUser)
                .Include(x => x.SenderUser)
                .Include(x => x.Message)
                .Where(x => x.ReceiverHasDeleted == false && x.ReceiverUserId == userId && x.Message.ParentId == null &&
                            (x.Message.Title.Contains(search) || x.Message.Body.Contains(search)
                            )));
        }

        public async Task<PagedList<UserMessage>> GetUserMessageListAsync(Guid receiverUserId, Guid senderUserId,
            int objectPerPage, int pageNumber)
        {
            return await GetPagedAsync(pageNumber, objectPerPage, GetAll().Include(x => x.ReceiverUser)
                .Include(x => x.SenderUser)
                .Include(x => x.Message).Where(x =>
                    (x.ReceiverUserId == receiverUserId && x.ReceiverHasDeleted == false &&
                     x.SenderUserId == senderUserId) ||
                    (x.ReceiverUserId == senderUserId && x.ReceiverHasDeleted == false &&
                     x.SenderUserId == receiverUserId))
                .OrderBy(x => x.SendDate));
        }

        public async Task<PagedList<UserMessage>> GetConversationListASync(int parentId, int objectPerPage,
            int pageNumber)
        {
            return await GetPagedAsync(pageNumber, objectPerPage, GetAll().Include(x => x.ReceiverUser)
                .Include(x => x.SenderUser)
                .Include(x => x.Message).Where(x =>
                    (x.MessageId == parentId || x.Message.ParentId == parentId) && x.ReceiverHasDeleted == false));
        }

        public async Task<UserMessage> GetReceiveMessage(Guid userId, int messageId)
        {
            return await GetAll().FirstOrDefaultAsync(x =>
                x.ReceiverHasDeleted == false && x.ReceiverUserId == userId && x.MessageId == messageId);
        }

        public async Task<UserMessage> GetSendMessage(Guid userId, int messageId)
        {
            return await GetAll().FirstOrDefaultAsync(x =>
                x.SenderHasDeleted == false && x.SenderUserId == userId && x.MessageId == messageId);
        }

        public async Task<UserMessage> GetMessageAsync(int id)
        {
            return await GetAll().Include(x => x.ReceiverUser)
                .Include(x => x.SenderUser).Include(x => x.Message)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}