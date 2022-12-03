using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MS.UnitOfWork.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MS.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {

        private readonly TContext _dbContext;

        private bool _disposed = false;

        /// <summary>
        /// 存储库
        /// </summary>
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(TContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 获取DbContext
        /// </summary>
        public TContext DbContext => _dbContext;


        /// <summary>
        /// 开始一个事务
        /// </summary>
        /// <returns></returns>
        public IDbContextTransaction BeginTransaction()
        {
            return _dbContext.Database.BeginTransaction();
        }

        /// <summary>
        /// 获取指定仓储
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="hasCustomRepository"></param>
        /// <returns></returns>
        public IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : class
        {
            if (_repositories==null)
            {
                _repositories = new Dictionary<Type, object>();
            }

            Type type = typeof(IRepository<TEntity>);

            if (!_repositories.TryGetValue(type,out object repo))// _repositories中是否存在TEntity类型的仓储
            {
                // 不存在
                // 创建新仓储
                IRepository<TEntity> newrepo = new Repository<TEntity>(_dbContext);
                // 将仓储添加到_repositories
                _repositories.Add(type,newrepo);

                return newrepo;
            }

            return (IRepository<TEntity>)repo;
        }

        /// <summary>
        /// 执行原生sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public int ExecuteSqlCommand(string sql, params object[] parameters)
            => _dbContext.Database.ExecuteSqlRaw(sql,parameters);


        public int SaveChanges()
            => _dbContext.SaveChanges();

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            //GC.SuppressFinalize (this)的作用:
            //实现IDisposable接口的对象可以
            //从IDisposable.Dispose方法调用此方法 (GC.SuppressFinalize (this))，
            //以防止垃圾回收器对不需要终止的对象调用 Object.Finalize。
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposeing) {
            if (!_disposed)
            {
                if (disposeing)
                {
                    if (_repositories!=null)
                    {
                        // 清除_repositories
                        _repositories.Clear();
                    }
                    // 释放资源
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        } 
    }
}
