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
    /// <summary>
    /// 通用仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity:class
    {
        #region GetAll

        /// <summary>
        /// 获取所有实体
        /// 注意性能！
        /// </summary>
        /// <returns>The <see cref="IQueryable{TEntity}"/>.</returns>
        IQueryable<TEntity> GetAll();


        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <param name="orderBy">排序</param>
        /// <param name="include">包含的导航属性</param>
        /// <param name="disableTracking">设置为true关闭追踪查询，默认为true</param>
        /// <param name="ignoreQueryFilters">设置为true忽略全局查询筛选过滤，默认为false</param>
        /// <returns></returns>
        IQueryable<TEntity> GetAll(Expression<Func<TEntity,bool>> predicate=null,
            Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>> orderBy=null,
            Func<IQueryable<TEntity>,IIncludableQueryable<TEntity,object>> include=null,
            bool disableTracking=true,
            bool ignoreQueryFilters=false
            );


        /// <summary>
        /// 获取所有实体，必须提供筛选谓词
        /// </summary>
        /// <typeparam name="TResult">输出数据类型</typeparam>
        /// <param name="selector">投影选择器</param>
        /// <param name="predicate">筛选谓词</param>
        /// <param name="orderBy">排序</param>
        /// <param name="include">包含的导航属性</param>
        /// <param name="disableTracking">设置为true关闭追踪查询，默认为true</param>
        /// <param name="ignoreQueryFilerts">设置为true关闭追踪查询。默认为true</param>
        /// <returns></returns>

        IQueryable<TResult> GetAll<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            bool ignoreQueryFilerts = false
            ) where TResult : class;



        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <param name="orderBy">排序</param>
        /// <param name="include">包含的导航属性</param>
        /// <param name="disableTracking">设置为true关闭追踪查询。默认为true</param>
        /// <param name="ignoreQueryFilters">设置为true忽略全局查询筛选过滤。默认为false</param>
        /// <returns></returns>
        Task<IList<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false
            );

        #endregion



        #region GetPagedList

        /// <summary>
        /// 获取分页数据
        /// 默认是关闭追踪查询的（拿到的数据默认只读）
        /// 默认开启全局查询筛选过滤
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <param name="orderBy">排序</param>
        /// <param name="include">包含的导航属性</param>
        /// <param name="pageIndex">当前页。默认第一页</param>
        /// <param name="pageSize">页大小。默认20笔数据</param>
        /// <param name="disableTracking">设置为true关闭追踪查询。默认为true</param>
        /// <param name="ignoreQueryFilters">设置为true忽略全局查询筛选过滤。默认为false</param>
        /// <returns></returns>
        IPagedList<TEntity> GetPagedList(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int pageIndex = 1,
            int pageSize = 20,
            bool disadisableTrackingble = true,
            bool ignoreQueryFilters = false
            );


        /// <summary>
        /// 获取分页数据
        /// 默认是关闭追踪查询的（拿到的数据默认只读）
        /// 默认开启全局查询筛选过滤
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <param name="orderBy">排序</param>
        /// <param name="include">包含的导航属性</param>
        /// <param name="pageIndex">当前页。默认第一页</param>
        /// <param name="pageSize">页大小。默认20笔数据</param>
        /// <param name="disableTracking">设置为true关闭追踪查询。默认为true</param>
        /// <param name="ignoreQueryFilters">设置为true忽略全局查询筛选过滤。默认为false</param>
        /// <param name="cancellationToken">异步token</param>
        /// <returns></returns>
        Task<IPagedList<TEntity>> GetPagedListAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int pageIndex = 1,
            int pageSize = 20,
            bool disableTracking = true,
            bool ignoreQueryFilters = false,
            CancellationToken cancellationToken = default
            );


        /// <summary>
        /// 获取分页数据
        /// 默认是关闭追踪查询的（拿到的数据默认只读）
        /// 默认开启全局查询筛选过滤
        /// </summary>
        /// <typeparam name="TResult">输出数据类型</typeparam>
        /// <param name="selector">投影选择器</param>
        /// <param name="predicate">条件表达式</param>
        /// <param name="orderBy">排序</param>
        /// <param name="include">包含的导航属性</param>
        /// <param name="pageIndex">当前页。默认第一页</param>
        /// <param name="pageSize">页大小。默认20笔数据</param>
        /// <param name="disableTracking">设置为true关闭追踪查询。默认为true</param>
        /// <param name="ignoreQueryFilters">设置为true忽略全局查询筛选过滤。默认为false</param>
        /// <param name="cancellationToken">异步token</param>
        /// <returns></returns>
        Task<IPagedList<TResult>> GetPagedListAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int pageIndex = 1,
            int pageSize = 20,
            bool disableTracking = true,
            bool ignoreQueryFilters = false,
            CancellationToken cancellationToken = default
            ) where TResult : class;


        /// <summary>
        /// 获取分页数据
        /// 默认是关闭追踪查询的（拿到的数据默认只读）
        /// 默认开启全局查询筛选过滤
        /// </summary>
        /// <typeparam name="TResult">输出数据类型</typeparam>
        /// <param name="selector">投影选择器</param>
        /// <param name="predicate">条件表达式</param>
        /// <param name="orderBy">排序</param>
        /// <param name="include">包含的导航属性</param>
        /// <param name="pageIndex">当前页。默认第一页</param>
        /// <param name="pageSize">页大小。默认20笔数据</param>
        /// <param name="disableTracking">设置为true关闭追踪查询。默认为true</param>
        /// <param name="ignoreQueryFilters">设置为true忽略全局查询筛选过滤。默认为false</param>
        /// <returns></returns>
        IPagedList<TResult> GetPagedList<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int pageIndex = 1,
            int pageSize = 20,
            bool disableTracking = true,
            bool ignoreQueryFilters = false
            );

        #endregion


        #region GetFirstOrDefault

        /// <summary>
        /// 获取满足条件的序列中的第一个元素
        /// 如果没有元素满足条件，则返回默认值
        /// 默认是关闭追踪查询的（拿到的数据默认只读）
        /// 默认开启全局查询筛选过滤
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <param name="orderBy">排序</param>
        /// <param name="include">排序</param>
        /// <param name="disableTracking">设置为true关闭追踪查询。默认为true</param>
        /// <param name="ignoreQueryFilters">设置为true忽略全局查询筛选过滤。默认为false</param>
        /// <returns></returns>
        TEntity GetFirstOrDefault(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false
            );

        /// <summary>
        /// 获取满足条件的序列中的第一个元素
        /// 如果没有元素满足条件，则返回默认值
        /// 默认是关闭追踪查询的（拿到的数据默认只读）
        /// 默认开启全局查询筛选过滤
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <param name="orderBy">排序</param>
        /// <param name="include">包含的导航属性</param>
        /// <param name="disableTracking">设置为true关闭追踪查询。默认为true</param>
        /// <param name="ignoreQueryFilters">设置为true忽略全局查询筛选过滤。默认为false</param>
        /// <param name="cancellationToken">异步token</param>
        /// <returns></returns>
        Task<TEntity> GetFirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy=null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false,
            CancellationToken cancellationToken=default
            );


        /// <summary>
        /// 获取满足条件的序列中的第一个元素
        /// 如果没有元素满足条件，则返回默认值
        /// 默认是关闭追踪查询的（拿到的数据默认只读）
        /// 默认开启全局查询筛选过滤
        /// </summary>
        /// <typeparam name="TResult">输出数据类型</typeparam>
        /// <param name="selector">投影选择器</param>
        /// <param name="predicate">条件表达式</param>
        /// <param name="orderBy">排序</param>
        /// <param name="include">包含的导航属性</param>
        /// <param name="disableTracking">设置为true关闭追踪查询。默认为true</param>
        /// <param name="ignoreQueryFilters">设置为true忽略全局查询筛选过滤。默认为false</param>
        /// <returns></returns>
        TResult GetFirstOrDefault<TResult>(
            Expression<Func<TEntity,TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false
            );



        /// <summary>
        /// 获取满足条件的序列中的第一个元素
        /// 如果没有元素满足条件，则返回默认值
        /// 默认是关闭追踪查询的（拿到的数据默认只读）
        /// 默认开启全局查询筛选过滤
        /// </summary>
        /// <typeparam name="TResult">输出数据类型</typeparam>
        /// <param name="selector">投影选择器</param>
        /// <param name="predicate">条件表达式</param>
        /// <param name="orderBy">排序</param>
        /// <param name="include">包含的导航属性</param>
        /// <param name="disableTracking">设置为true关闭追踪查询。默认为true</param>
        /// <param name="ignoreQueryFilters">设置为true忽略全局查询筛选过滤。默认为false</param>
        /// <param name="cancellationToken">异步token</param>
        /// <returns></returns>
        Task<TResult> GetFirstOrDefaultAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false,
            CancellationToken cancellationToken = default
            );


        #endregion


        #region Find

        /// <summary>
        /// 查找具有给定主键值的实体。如果找到，将附加到上下文并返回。如果未找到实体，则返回null
        /// </summary>
        /// <param name="keyValues">要查找的实体的主键值。</param>
        /// <returns>任务结果包含找到的实体或null。</returns>
        TEntity Find(params object[] keyValues);


        /// <summary>
        /// 查找具有给定主键值的实体。如果找到，将附加到上下文并返回。如果未找到实体，则返回null
        /// </summary>
        /// <param name="keyValues">要查找的实体的主键值</param>
        /// <returns>表示异步查找操作的<see cref="Task{TEntity}"/>。任务结果包含找到的实体或null</returns>
        ValueTask<TEntity> FindAsync(params object[] keyValues);


        /// <summary>
        /// 查找具有给定主键值的实体。如果找到，将附加到上下文并返回。如果未找到实体，则返回null
        /// </summary>
        /// <param name="keyValues">要查找的实体的主键值</param>
        /// <param name="cancellationToken">等待任务完成时需要观察的<see cref="CancellationToken"/></param>
        /// <returns>表示异步查找操作的<see cref="Task{TEntity}"/>。任务结果包含找到的实体或null</returns>
        ValueTask<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken);


        #endregion

        #region sql、count、exist

        /// <summary>
        /// 使用原生sql查询来获取指定数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IQueryable<TEntity> FromSql(string sql, params object[] parameters);

        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 按指定条件元素是否存在
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool Exists(Expression<Func<TEntity, bool>> predicate = null);

        #endregion

        #region Insert

        /// <summary>
        /// 同步插入新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Inster(TEntity entity);


        /// <summary>
        /// 同步插入多个实体
        /// </summary>
        /// <param name="entities">插入的实体</param>
        void Inster(params TEntity[] entities);


        /// <summary>
        /// 同步插入多个实体
        /// </summary>
        /// <param name="entities">插入的实体</param>
        void Inster(IEnumerable<TEntity> entities);


        /// <summary>
        /// 异步插入新实体
        /// </summary>
        /// <param name="entity">插入的实体</param>
        /// <param name="cancellationToken"></param>
        /// <returns>表示异步插入操作的<see cref="Task"/></returns>
        ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);


        /// <summary>
        /// 异步插入多个实体
        /// </summary>
        /// <param name="entities">插入的实体</param>
        /// <returns>表示异步插入操作的<see cref="Task"/></returns>
        Task InsterAsync(params TEntity[] entities);

        /// <summary>
        /// 异步插入多个实体
        /// </summary>
        /// <param name="entities">插入的实体</param>
        /// <param name="cancellationToken">等待任务完成时需要观察的<see cref="CancellationToken"/></param>
        /// <returns></returns>
        Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        #endregion

        #region Update

        /// <summary>
        /// 更新指定的实体
        /// </summary>
        /// <param name="entity">要更新的实体</param>
        void Update(TEntity entity);


        /// <summary>
        /// 更新多个实体
        /// </summary>
        /// <param name="entities">要更新的实体</param>
        void Update(params TEntity[] entities);

        /// <summary>
        /// 更新多个实体
        /// </summary>
        /// <param name="entities">要更新的实体</param>
        void Update(IEnumerable<TEntity> entities);

        #endregion

        #region Delete

        /// <summary>
        /// 按指定的主键删除实体
        /// </summary>
        /// <param name="id">主键值</param>
        void Delete(object id);


        /// <summary>
        /// 删除指定实体
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        void Delete(TEntity entity);

        /// <summary>
        /// 删除多个实体
        /// </summary>
        /// <param name="entities">要删除的实体</param>
        void Delete(params TEntity[] entities);

        /// <summary>
        /// 删除多个实体
        /// </summary>
        /// <param name="entities">要删除的实体</param>
        void Delete(IEnumerable<TEntity> entities);
        #endregion
    }
}
