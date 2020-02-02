using System;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Comment;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;
using Hastnama.GuitarIranShop.DataAccess.Helper;

namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface ICommentService : IRepository<Comment>
    {
        Task<Result<PagedList<CommentDto>>> List(PagingOptions pagingOptions);
        Task<Result> Update(UpdateCommentDto updateCommentDto);
        Task<Result<CommentDto>> Create(CreateCommentDto dto);
        Task<Result<CommentDto>> Get(Guid id);
    }
}