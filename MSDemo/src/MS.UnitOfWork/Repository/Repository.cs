using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using MS.UnitOfWork.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MS.UnitOfWork.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = dbContext.Set<TEntity>();
        }

        #region GetAll
        public IQueryable<TEntity> GetAll() => _dbSet;

        public IQueryable<TEntity> GetAll(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false)
        {
            IQueryable<TEntity> query = _dbSet;

            if (disableTracking)
            {
                query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (ignoreQueryFilters)
            {
                query = query.IgnoreQueryFilters();
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            return query;
        }

        public IQueryable<TResult> GetAll<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, 
            bool disableTracking = true,
            bool ignoreQueryFilerts = false) where TResult : class
        {
            IQueryable<TEntity> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate!=null)
            {
                query = query.Where(predicate);
            }

            if (ignoreQueryFilerts)
            {
                query = query.IgnoreQueryFilters();
            }

            if (orderBy!=null)
            {
                return orderBy(query).Select(selector);
            }

            return query.Select(selector);
        }

        public async Task<IList<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false)
        {
            IQueryable<TEntity> query = _dbSet;

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include!=null)
            {
                query = include(query);
            }

            if (predicate!=null)
            {
                query = query.Where(predicate);
            }

            if (ignoreQueryFilters)
            {
                query = query.IgnoreQueryFilters();
            }

            if (orderBy!=null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }
        #endregion


        #region GetPagedList
        public virtual IPagedList<TEntity> GetPagedList(
            Expression<Func<TEntity, bool>> predicate = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, 
            int pageIndex = 1, 
            int pageSize = 20, 
            bool disadisableTrackingble = true, 
            bool ignoreQueryFilters = false)
        {
            IQueryable<TEntity> query = _dbSet;

            if (disadisableTrackingble)
            {
                query = query.AsNoTracking();
            }

            if (include!=null)
            {
                query = include(query);
            }

            if (predicate!=null)
            {
                query = query.Where(predicate);
            }

            if (ignoreQueryFilters)
            {
                query = query.IgnoreQueryFilters();
            }

            if (orderBy!=null)
            {
                return orderBy(query).ToPagedList(pageIndex,pageSize);
            }

            return query.ToPagedList(pageIndex, pageSize);
        }

        public virtual IPagedList<TResult> GetPagedList<TResult>(
            Expression<Func<TEntity, TResult>> selector, 
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int pageIndex = 1, 
            int pageSize = 20, 
            bool disableTracking = true,
            bool ignoreQueryFilters = false)
        {
            IQueryable<TEntity> query = _dbSet;

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include!=null)
            {
                query = include(query);
            }

            if (predicate!=null)
            {
                query = query.Where(predicate);
            }

            if (ignoreQueryFilters)
            {
                query = query.IgnoreQueryFilters();
            }

            if (orderBy!=null)
            {
                return orderBy(query).Select(selector).ToPagedList(pageIndex,pageSize);
            }

            return query.Select(selector).ToPagedList(pageIndex, pageSize);
        }

        public virtual async Task<IPagedList<TEntity>> GetPagedListAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int pageIndex = 1, 
            int pageSize = 20, 
            bool disableTracking = true,
            bool ignoreQueryFilters = false, 
            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = _dbSet;

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include!=null)
            {
                query = include(query);
            }

            if (predicate!=null)
            {
                query = query.Where(predicate);
            }

            if (ignoreQueryFilters)
            {
                query = query.IgnoreQueryFilters();
            }

            if (orderBy!=null)
            {
                return await orderBy(query).ToPagedListAsync(pageIndex,pageSize);
            }

            return await query.ToPagedListAsync(pageIndex, pageSize);
        }

        public virtual async Task<IPagedList<TResult>> GetPagedListAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int pageIndex = 1, 
            int pageSize = 20, 
            bool disableTracking = true, 
            bool ignoreQueryFilters = false,
            CancellationToken cancellationToken = default) where TResult : class
        {
            IQueryable<TEntity> query = _dbSet;

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include!=null)
            {
                query = include(query);
            }

            if (predicate !=null)
            {
                query = query.Where(predicate);
            }

            if (ignoreQueryFilters)
            {
                query = query.IgnoreQueryFilters();
            }

            if (orderBy!=null)
            {
                return await orderBy(query).Select(selector).ToPagedListAsync(pageIndex, pageSize); 
            }

            return await query.Select(selector).ToPagedListAsync(pageIndex, pageSize);

        }
        #endregion

        #region GetFirstOrDefault 
        public virtual TEntity GetFirstOrDefault(
            Expression<Func<TEntity, bool>> predicate = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false)
        {
            IQueryable<TEntity> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include!=null)
            {
                query = include(query);
            }

            if (predicate!=null)
            {
                query = query.Where(predicate);
            }

            if (ignoreQueryFilters)
            {
                query = query.IgnoreQueryFilters();
            }

            if (orderBy!=null)
            {
                return orderBy(query).FirstOrDefault();
            }

            return query.FirstOrDefault();

        }

        public virtual TResult GetFirstOrDefault<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false)
        {
            IQueryable<TEntity> query = _dbSet;

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include!=null)
            {
                query = include(query);
            }

            if (predicate!=null)
            {
                query = query.Where(predicate);
            }

            if (ignoreQueryFilters)
            {
                query = query.IgnoreQueryFilters();
            }

            if (orderBy!=null)
            {
                return orderBy(query).Select(selector).FirstOrDefault();
            }

            return query.Select(selector).FirstOrDefault();
        }

        public async virtual Task<TEntity> GetFirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true, 
            bool ignoreQueryFilters = false, 
            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = _dbSet;

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include!=null)
            {
                query = include(query);
            }

            if (predicate!=null)
            {
                query = query.Where(predicate);
            }

            if (ignoreQueryFilters)
            {
                query = query.IgnoreQueryFilters();
            }

            if (orderBy!=null)
            {
                return await orderBy(query).FirstOrDefaultAsync(cancellationToken);
            }

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public async virtual Task<TResult> GetFirstOrDefaultAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector, 
            Expression<Func<TEntity, bool>> predicate = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false, 
            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include!=null)
            {
                query = include(query);
            }

            if (predicate!=null)
            {
                query = query.Where(predicate);
            }

            if (ignoreQueryFilters)
            {
                query = query.IgnoreQueryFilters();
            }

            if (orderBy!=null)
            {
                return await orderBy(query).Select(selector).FirstOrDefaultAsync(cancellationToken);
            }

            return await query.Select(selector).FirstOrDefaultAsync(cancellationToken);
        }
        #endregion


        #region Find
        public TEntity Find(params object[] keyValues) => _dbSet.Find(keyValues);

        public ValueTask<TEntity> FindAsync(params object[] keyValues) => _dbSet.FindAsync(keyValues);

        public ValueTask<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken) => _dbSet.FindAsync(cancellationToken);
        #endregion

        #region sql、count、exist
        public int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate!=null)
            {
                return _dbSet.Count(predicate);
            }
            return _dbSet.Count();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate!=null)
            {
                return await _dbSet.CountAsync(predicate);
            }
            return await _dbSet.CountAsync();
        }

        public bool Exists(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate!=null)
            {
                return _dbSet.Any(predicate);
            }

            return _dbSet.Any();
        }

        public IQueryable<TEntity> FromSql(string sql, params object[] parameters) => _dbSet.FromSqlRaw(sql, parameters);
        #endregion

        #region Inster
        public ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
            => _dbSet.AddAsync(entity, cancellationToken);

        public Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
            => _dbSet.AddRangeAsync(entities, cancellationToken);

        public TEntity Inster(TEntity entity)
            => _dbSet.Add(entity).Entity;

        public void Inster(params TEntity[] entities)
            => _dbSet.AddRange(entities);

        public void Inster(IEnumerable<TEntity> entities)
            => _dbSet.AddRange(entities);

        public Task InsterAsync(params TEntity[] entities)
            => _dbSet.AddRangeAsync(entities);
        #endregion


        #region Update
        public void Update(TEntity entity)
            => _dbSet.Update(entity);

        public void Update(params TEntity[] entities)
            => _dbSet.UpdateRange(entities);

        public void Update(IEnumerable<TEntity> entities)
            => _dbSet.UpdateRange(entities);
        #endregion

        #region Delete

        public void Delete(object id)
        {
           var entity = _dbSet.Find(id);

            if (entity!=null)
            {
                Delete(entity);
            }
        }

        public void Delete(TEntity entity)
            => _dbSet.Remove(entity);

        public void Delete(params TEntity[] entities)
            => _dbSet.RemoveRange(entities);

        public void Delete(IEnumerable<TEntity> entities)
            => _dbSet.RemoveRange(entities);
        #endregion












    }
}
