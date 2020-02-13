using System;
using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Comment;
using Hastnama.Ekipchi.DataAccess.Context;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hastnama.Ekipchi.Business.Service.Class
{
    public class CommentService : Repository<EkipchiDbContext, Comment>, ICommentService
    {
        private readonly IMapper _mapper;

        public CommentService(EkipchiDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Result<PagedList<CommentDto>>> List(FilterCommentQueryDto filterQueryDto)
        {
            var comments = await WhereAsyncAsNoTracking(c =>
                    (filterQueryDto.IsConfirmed == null || c.IsConfirmed == filterQueryDto.IsConfirmed)
                    && (filterQueryDto.UserId == null || c.UserId == filterQueryDto.UserId),
                filterQueryDto,
                c => c.User, c => c.ParentComment, c => c.Children);

            return Result<PagedList<CommentDto>>.SuccessFull(comments.MapTo<CommentDto>(_mapper));
        }

        public async Task<Result> Update(UpdateCommentDto updateCommentDto)
        {
            var comment = await FirstOrDefaultAsync(c => c.Id == updateCommentDto.Id);
            _mapper.Map(updateCommentDto, comment);
            await Context.SaveChangesAsync();

            return Result.SuccessFull();
        }

        public async Task<Result<CommentDto>> Create(CreateCommentDto createCommentDto,Guid userId)
        {
            var user = await Context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return Result<CommentDto>.Failed(new NotFoundObjectResult(new ApiMessage
                    {Message = ResponseMessage.UserNotFound}));

            var eEvent = await Context.Events.FirstOrDefaultAsync(u => u.Id == createCommentDto.EventId);
            if (eEvent == null)
                return Result<CommentDto>.Failed(new NotFoundObjectResult(new ApiMessage
                    {Message = ResponseMessage.UserNotFound}));

            var comment = _mapper.Map<Comment>(createCommentDto);
            comment.User = user;
            comment.Event = eEvent;

            await AddAsync(comment);
            await Context.SaveChangesAsync();

            return Result<CommentDto>.SuccessFull(_mapper.Map<CommentDto>(comment));
        }

        public async Task<Result<CommentDto>> Get(Guid id)
        {
            var comment = await FirstOrDefaultAsyncAsNoTracking(c => c.Id == id, c => c.User, c => c.ParentComment,
                c => c.Children);
            if (comment == null)
                return Result<CommentDto>.Failed(new NotFoundObjectResult(
                    new ApiMessage
                        {Message = ResponseMessage.CommentNotFound}));

            return Result<CommentDto>.SuccessFull(_mapper.Map<CommentDto>(comment));
        }

        public async Task<Result> Delete(Guid id)
        {
            var comment = await FirstOrDefaultAsyncAsNoTracking(c => c.Id == id,
                c => c.Children);
            if (comment == null)
                return Result.Failed(new NotFoundObjectResult(
                    new ApiMessage
                        {Message = ResponseMessage.CommentNotFound}));

            RemoveRange(comment.Children);
            Delete(comment);
            await Context.SaveChangesAsync();

            return Result.SuccessFull();
        }
    }
}