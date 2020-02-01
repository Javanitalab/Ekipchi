using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Helper;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Hastnama.Ekipchi.DataAccess.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> FirstOrDefaultAsyncAsNoTracking(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, bool>> include= null);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,Expression<Func<TEntity, bool>> include = null);

        Task<List<TEntity>> WhereAsyncAsNoTracking(Expression<Func<TEntity, bool>> predicate, PagingOptions pagingOptions, Expression<Func<TEntity, bool>> include = null);

        Task<List<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate, PagingOptions pagingOptions, Expression<Func<TEntity, bool>> include = null);
    
        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);

        ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity);

        void Add(TEntity entity);

        Task AddRange(IList<TEntity> tEntities);

        void Delete(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        void Edit(TEntity entity);

        Task<PagedList<TEntity>> GetPagedAsync(int pageNumber, int pageSize, IQueryable<TEntity> query);
    }
}