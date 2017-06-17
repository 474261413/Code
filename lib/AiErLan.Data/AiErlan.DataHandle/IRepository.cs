using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AiErLan.DataHandle
{
    /// <summary>
    /// 数据存取操作接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository
    {
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> FindAll<TEntity>() where TEntity : class;
        /// <summary>
        /// 获取指定条件的集合
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<TEntity> Filter<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        /// <summary>
        /// 获取指定条件的集合,并进行分页
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="total"></param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        IQueryable<TEntity> Filter<TEntity>(Expression<Func<TEntity, bool>> filter, out int total, int index = 0, int size = 50) where TEntity : class;
        /// <summary>
        /// 是否包含某个实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool Contains<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        /// <summary>
        /// 以ID获取指定实体
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        TEntity Find<TEntity>(params object[] keys) where TEntity : class;
        /// <summary>
        /// 以指定条件获取实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity Find<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        /// <summary>
        /// 添加指实体到数据库
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Add<TEntity>(TEntity t) where TEntity : class;
        /// <summary>
        /// 删除指定实体
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Remove<TEntity>(TEntity t) where TEntity : class;
        /// <summary>
        /// 以指定的条件删除实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool Remove<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        /// <summary>
        /// 更新指定的实体
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Update<TEntity>(TEntity t) where TEntity : class;
        /// <summary>
        /// 获取指定条件的某个实体
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        TEntity Single<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKProperty"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetPaged<TEntity, TKProperty>(int pageIndex, int pageSize,
                                                           Expression<Func<TEntity, bool>> filter,
                                                           Expression<Func<TEntity, TKProperty>> orderBy, bool ascending,
                                                           out int total) where TEntity : class;

        /// <summary>
        /// Sql语句查询
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sqlQuery"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters) where TEntity : class;
    }
}
