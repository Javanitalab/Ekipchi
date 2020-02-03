using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.DataAccess.Extensions;
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

        public async Task<TEntity> FirstOrDefaultAsyncAsNoTracking(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>().AsNoTracking().AsQueryable<TEntity>();
            Expression<Func<TEntity, object>>[] expressionArray = includes;
            for (int index = 0; index < expressionArray.Length; ++index)
            {
                Expression<Func<TEntity, object>> include = expressionArray[index];
                query = (IQueryable<TEntity>) EntityFrameworkQueryableExtensions.Include<TEntity>((IQueryable<TEntity>) query, include.AsPath());
            }
            return await (Task<TEntity>) EntityFrameworkQueryableExtensions.FirstOrDefaultAsync<TEntity>((IQueryable<TEntity>) query, predicate, new CancellationToken());
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>().AsQueryable<TEntity>();
            Expression<Func<TEntity, object>>[] expressionArray = includes;
            for (int index = 0; index < expressionArray.Length; ++index)
            {
                Expression<Func<TEntity, object>> include = expressionArray[index];
                query = (IQueryable<TEntity>) EntityFrameworkQueryableExtensions.Include<TEntity>((IQueryable<TEntity>) query, include.AsPath());
            }
            return await (Task<TEntity>) EntityFrameworkQueryableExtensions.FirstOrDefaultAsync<TEntity>((IQueryable<TEntity>) query, predicate, new CancellationToken());
        }

        public async Task<PagedList<TEntity>> WhereAsyncAsNoTracking(Expression<Func<TEntity, bool>> predicate,
            PagingOptions pagingOptions,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = ((IQueryable<TEntity>) Context.Set<TEntity>()).AsNoTracking().Where<TEntity>(predicate).AsQueryable<TEntity>();
            Expression<Func<TEntity, object>>[] expressionArray = includes;
            for (int index = 0; index < expressionArray.Length; ++index)
            {
                Expression<Func<TEntity, object>> include = expressionArray[index];
                query = (IQueryable<TEntity>) EntityFrameworkQueryableExtensions.Include<TEntity>((IQueryable<TEntity>) query, include.AsPath());
            }
            return await GetPagedAsync(pagingOptions.Page, pagingOptions.Limit, query);
        }

        public async Task<PagedList<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate,
            PagingOptions pagingOptions,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = ((IQueryable<TEntity>) Context.Set<TEntity>()).Where<TEntity>(predicate).AsQueryable<TEntity>();
            Expression<Func<TEntity, object>>[] expressionArray = includes;
            for (int index = 0; index < expressionArray.Length; ++index)
            {
                Expression<Func<TEntity, object>> include = expressionArray[index];
                query = (IQueryable<TEntity>) EntityFrameworkQueryableExtensions.Include<TEntity>((IQueryable<TEntity>) query, include.AsPath());
            }
            return await GetPagedAsync(pagingOptions.Page, pagingOptions.Limit, query);
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

        private static string GetPropertyName<TEntity>(Expression<Func<TEntity, object>> expression)
        {
            MemberExpression memberExpr = expression.Body as MemberExpression;
            if (memberExpr == null)
                throw new ArgumentException("Expression body must be a member expression");
            return memberExpr.Member.Name;
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