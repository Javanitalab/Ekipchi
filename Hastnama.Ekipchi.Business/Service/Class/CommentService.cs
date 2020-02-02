using System;
using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Comment;
using Hastnama.Ekipchi.DataAccess.Context;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;
using Hastnama.GuitarIranShop.DataAccess.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Hastnama.Ekipchi.Business.Service.Class
{
    public class CommentService : Repository<EkipchiDbContext, Comment>, ICommentService
    {
        private readonly IMapper _mapper;

        public CommentService(EkipchiDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Result<PagedList<CommentDto>>> List(PagingOptions pagingOptions)
        {
            var cities = await WhereAsyncAsNoTracking(c => true, pagingOptions,
                c => c.User, c => c.ParentComment, c => c.Children);

            return Result<PagedList<CommentDto>>.SuccessFull(cities.MapTo<CommentDto>(_mapper));
        }

        public async Task<Result> Update(UpdateCommentDto updateCommentDto)
        {

            var comment = await FirstOrDefaultAsync(c => c.Id == updateCommentDto.Id);
            _mapper.Map(updateCommentDto, comment);
            await Context.SaveChangesAsync();

            return Result.SuccessFull();
        }

        public async Task<Result<CommentDto>> Create(CreateCommentDto createCommentDto)
        {
            var comment = _mapper.Map(createCommentDto, new Comment());
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
                        {Message = PersianErrorMessage.CommentNotFound}));

            return Result<CommentDto>.SuccessFull(_mapper.Map<CommentDto>(comment));
        }
    }
}