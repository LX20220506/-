using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MS.UnitOfWork.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MS.UnitOfWork
{
    public interface IUnitOfWork<TContext>:IDisposable where TContext:DbContext
    {
        /// <summary>
        /// 获取DbContext
        /// </summary>
        TContext DbContext { get; }

        /// <summary>
        /// 开始一个事务
        /// </summary>
        /// <returns></returns>
        IDbContextTransaction BeginTransaction();


        /// <summary>
        /// 获取指定仓储
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="hasCustomRepository">如有自定义仓储设为True</param>
        /// <returns></returns>
        IRepository<TEntity> GetReposirory<TEntity>(bool hasCustomRepository=false) where TEntity:class;


        /// <summary>
        ///  DbContext提交修改
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        ///  DbContext提交修改（异步）
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();


        /// <summary>
        /// 执行原生sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        int ExecuteSqlCommand(string sql, params object[] parameters);
    }
}
