using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Hastnama.Ekipchi.DataAccess.Repository
{
    public class Repository<TContext, TEntity> : IDisposable,
        IRepository<TEntity>
        where TEntity : class
        where TContext : DbContext, new()

    {
        public TContext Context { get; set; }

        public Repository(TContext context)
        {
            Context = context;
        }

        public virtual ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity)
        {
            return Context.Set<TEntity>().AddAsync(entity);
        }


        public void Add(TEntity entity)
        {
            Context.Add(entity);
        }

        public virtual Task AddRange(IList<TEntity> tEntities)
        {
            return Context.Set<TEntity>().AddRangeAsync(tEntities);
        }

        public void Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public void Edit(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public Task<TEntity> FirstOrDefaultAsyncAsNoTracking(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, bool>> include = null)
        {
            return Context.Set<TEntity>().AsNoTracking().Include(include).FirstOrDefaultAsync(predicate);
        }

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public Task<List<TEntity>> WhereAsyncAsNoTracking(Expression<Func<TEntity, bool>> predicate,
            PagingOptions pagingOptions,
            Expression<Func<TEntity, bool>> include = null
        )
        {
            return Context.Set<TEntity>().AsNoTracking().Include(include).Where(predicate)
                .Skip(pagingOptions.Page * pagingOptions.Limit).Take(pagingOptions.Limit).ToListAsync();
        }

        public Task<List<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate,
            PagingOptions pagingOptions,
            Expression<Func<TEntity, bool>> include = null)
        {
            return Context.Set<TEntity>().Include(include).Where(predicate)
                .Skip(pagingOptions.Page * pagingOptions.Limit).Take(pagingOptions.Limit).ToListAsync();
            ;
        }

        public IQueryable<TEntity> GetAll()
        {
            return Context.Set<TEntity>();
        }

        public async Task<PagedList<TEntity>> GetPagedAsync(int pageNumber, int pageSize, IQueryable<TEntity> query)
        {
            if (pageSize <= 0)
                pageSize = 10;

            var rowsCount = query.Count();

            if (rowsCount <= pageSize || pageNumber <= 1)
                pageNumber = 1;

            return await PagedList<TEntity>.CreateAsync(query, pageNumber, pageSize, rowsCount);
        }


        public PagedList<TEntity> GetPagedAsync(int pageNumber, int pageSize, IEnumerable<TEntity> query)
        {
            if (pageSize <= 0)
                pageSize = 10;

            var rowsCount = query.Count();

            if (rowsCount <= pageSize || pageNumber <= 1)
                pageNumber = 1;

            return PagedList<TEntity>.CreateAsync(query, pageNumber, pageSize, rowsCount);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}