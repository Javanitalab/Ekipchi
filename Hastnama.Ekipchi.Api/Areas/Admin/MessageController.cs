using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Api.Core.Extensions;
using Hastnama.Ekipchi.Business.Service;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Message;
using Hastnama.Ekipchi.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Message = Hastnama.Ekipchi.DataAccess.Entities.Message;

namespace Hastnama.Ekipchi.Api.Areas.Admin
{
    public class MessageController : BaseAdminController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MessageController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        /// <summary>
        /// Get Message List
        /// </summary>
        /// <param name="pagingOptions"></param>
        /// <param name="query"></param>
        /// <param name="receive"></param>
        /// <returns>Get List Send and Receive message</returns>
        /// <response code="200">if message Successfully return</response>
        /// <response code="400">If out of range page and limit</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesErrorResponseType(typeof(ApiMessage))]
        [Produces(typeof(PagedList<UserMessageDto>))]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PagingOptions pagingOptions, string query, bool receive)
        {
            PagedList<UserMessage> messages;

            if (receive)
                messages = await _unitOfWork.UserMessageService.GetReceiveMessageListAsync(pagingOptions,
                    User.GetUserId(), query);
            else
                messages = await _unitOfWork.UserMessageService.GetSendMessageListAsync(pagingOptions, User.GetUserId(),
                    query);

            return Ok(messages.MapTo<UserMessageDto>(_mapper));
        }


        /// <summary>
        /// get Message
        /// </summary>
        /// <returns>no Content</returns>
        /// <response code="200">ig get message Successfully</response>
        /// <response code="404">if message not found or not for this user</response>
        /// <response code="500">If an unexpected error happen</response>
        [HttpGet("{id}", Name = "GetMessage")]
        public async Task<IActionResult> Get(int id)
        {
            var message = await _unitOfWork.MessageService.GetMessageAsync(User.GetUserId(), id);

            if (message == null)
                return NotFound(new ApiMessage {Message = PersianErrorMessage.MessageNotFound});

            await _unitOfWork.SaveChangesAsync();

            return Ok(_mapper.Map<MessageDto>(message));
        }


        /// <summary>
        /// Delete Message
        /// </summary>
        /// <returns>no Content</returns>
        /// <response code="204">ig get message Successfully</response>
        /// <response code="404">if message not found or not for this user</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesErrorResponseType(typeof(ApiMessage))]
        [Produces(typeof(PagedList<MessageDto>))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, bool isReceive)
        {
            #region Delete Reciver Message

            if (isReceive)
            {
                var receiverMessage = await _unitOfWork.UserMessageService.GetReceiveMessage(User.GetUserId(), id);

                if (receiverMessage == null)
                    return NotFound(new ApiMessage {Message = PersianErrorMessage.MessageNotFound});

                receiverMessage.ReceiverHasDeleted = true;
                _unitOfWork.UserMessageService.Edit(receiverMessage);

                await _unitOfWork.SaveChangesAsync();

                return NoContent();
            }

            #endregion

            #region delete sender

            var senderMessage = await _unitOfWork.UserMessageService.GetSendMessage(User.GetUserId(), id);

            if (senderMessage == null)
                return NotFound(new ApiMessage {Message = PersianErrorMessage.MessageNotFound});

            senderMessage.SenderHasDeleted = true;

            _unitOfWork.UserMessageService.Edit(senderMessage);

            await _unitOfWork.SaveChangesAsync();

            return NoContent();

            #endregion
        }


        /// <summary>
        /// Send Message
        /// </summary>
        /// <returns>Create Message</returns>
        /// <response code="201">if Send message Successfully</response>
        /// <response code="400">if validation Failed or sender and receiver has same id</response>
        /// <response code="404">if user Not Found for Send Message or if Parent Id not Found</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesErrorResponseType(typeof(ApiMessage))]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateMessageDto messageDto)
        {
            #region validation

            var result = await _unitOfWork.UserService.GetByEmail(messageDto.Email);

            var user = result.Data;

            if (!result.Success || user == null)
                return NotFound(new ApiMessage {Message = PersianErrorMessage.UserNotFound});

            if (user.Id == User.GetUserId())
                return NotFound(new ApiMessage {Message = PersianErrorMessage.SenderAndReceiverAreTheSame});

            if (messageDto.ParentId.HasValue)
                if (!await _unitOfWork.MessageService.IsMessageExist(messageDto.ParentId.Value))
                    return NotFound(new ApiMessage {Message = PersianErrorMessage.ParentMessageNotFound});

            #endregion


            var message = _mapper.Map<Message>(messageDto);

            await _unitOfWork.MessageService.AddAsync(message);

            var userMessage = new UserMessage
            {
                SenderUserId = User.GetUserId(),
                ReceiverUserId = user.Id,
                SendDate = DateTime.Now,
                MessageId = message.Id,
            };

            await _unitOfWork.UserMessageService.AddAsync(userMessage);

            await _unitOfWork.SaveChangesAsync();

            return Created(Url.Link("GetMessage", new {message.Id}),
                _mapper.Map<MessageDto>(message));
        }

        /// <summary>
        /// Get Conversation
        /// </summary>
        /// <returns>no Content</returns>
        /// <response code = "200" > if get Conversation Successfully</response>
        /// <response code = "400" >if out of range page and limit</response>
        /// <response code = "404" >if Receiver User  not found </response>
        /// <response code = "500" > If an unexpected error happen</response>
        [ProducesResponseType(typeof(PagedList<MessageDto>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesErrorResponseType(typeof(ApiMessage))]
        [Produces(typeof(PagedList<UserMessageDto>))]
        [HttpGet("{id}/conversation")]
        public async Task<IActionResult> Get([FromQuery] PagingOptions pagingOptions, int id)
        {
            if (!await _unitOfWork.MessageService.IsMessageExist(id))
                return NotFound(new ApiMessage {Message = PersianErrorMessage.MessageNotFound});

            var conversation =
                await _unitOfWork.UserMessageService.GetConversationListASync(id, pagingOptions.Limit.Value,
                    pagingOptions.Page.Value);

            return Ok(conversation.MapTo<UserMessageDto>(_mapper));
        }


        /// <summary>
        /// Read Message
        /// </summary>
        /// <returns>no Content</returns>
        /// <response code="204">if read Message Successfully</response>
        /// <response code="404">if message not found or not for this user or parent id was wrong</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesErrorResponseType(typeof(ApiMessage))]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id)
        {
            var message = await _unitOfWork.UserMessageService.GetReceiveMessage(User.GetUserId(), id);

            if (message == null)
                return NotFound(new ApiMessage {Message = PersianErrorMessage.MessageNotFound});

            message.SeenDate = DateTime.Now;

            _unitOfWork.UserMessageService.Edit(message);

            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }


        [HttpPost("{id}/Forward")]
        public async Task<IActionResult> Post(int id, [FromBody] Guid[] userReceiverId)
        {
            #region validation

            if (userReceiverId is null)
                return BadRequest(new ApiMessage {Message = PersianErrorMessage.ReceiverNotSet});

            if (!await _unitOfWork.MessageService.IsMessageExist(id))
                return NotFound(new ApiMessage {Message = PersianErrorMessage.MessageNotFound});

            #endregion

            List<UserMessage> userMessages = new List<UserMessage>();

            foreach (var user in userReceiverId)
            {
                if (await _unitOfWork.UserService.Get(user) is null)
                    return NotFound(new ApiMessage {Message = PersianErrorMessage.UserNotFound});

                userMessages.Add(new UserMessage
                    {ReceiverUserId = user, SendDate = DateTime.Now, SenderUserId = User.GetUserId(), MessageId = id});
            }

            await _unitOfWork.UserMessageService.AddRange(userMessages);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }


        /// <summary>
        /// Read Message
        /// </summary>
        /// <returns>no Content</returns>
        /// <response code="204">if read Message Successfully</response>
        /// <response code="404">if message not found or not for this user or parent id was wrong</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(UserMessageDto), 201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesErrorResponseType(typeof(ApiMessage))]
        [HttpPost("{userMessageId}/ReplyTo")]
        public async Task<IActionResult> Post(int userMessageId, [FromBody] CreateReplyTo createReplyTo)
        {
            #region validation

            var userMessage = await _unitOfWork.UserMessageService.GetMessageAsync(userMessageId);

            if (userMessage is null)
                return NotFound(new ApiMessage {Message = PersianErrorMessage.MessageNotFound});

            if (userMessage.ReceiverUserId != User.GetUserId() || userMessage.SenderUserId != User.GetUserId())
            {
                if (createReplyTo.ParentId.HasValue)
                    if (!await _unitOfWork.MessageService.IsMessageExist(createReplyTo.ParentId.Value))
                        return NotFound(new ApiMessage {Message = PersianErrorMessage.ParentMessageNotFound});

                #endregion

                var message = _mapper.Map<Message>(createReplyTo);

                var receiverId = userMessage.ReceiverUserId == User.GetUserId()
                    ? userMessage.SenderUserId
                    : userMessage.ReceiverUserId;

                await _unitOfWork.MessageService.AddAsync(message);

                var userMessages = new UserMessage
                {
                    SenderUserId = User.GetUserId(),
                    ReceiverUserId = receiverId.Value,
                    SendDate = DateTime.Now,
                    MessageId = message.Id,
                };

                await _unitOfWork.UserMessageService.AddAsync(userMessages);
                await _unitOfWork.SaveChangesAsync();

                return Created(Url.Link("GetMessage", new {message.Id}),
                    _mapper.Map<UserMessageDto>(userMessages, opt => opt.Items["lang"] = "fa-IR"));
            }

            return BadRequest(new ApiMessage {Message = PersianErrorMessage.UnAuthorized});
        }
    }
}