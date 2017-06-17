using AiErLan.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AiErLan.DataHandle
{
    /// <summary>
    /// mssql操作基类，主要针对sql server数据库
    /// </summary>
    /// <typeparam name="Entities"></typeparam>
    public class DataOperate  : IRepository/*, IDisposable*/ 
    {
        protected bool IsDisposable = false;
        public Entities dbContext { set; get; }

        #region inilitization unini

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="context">数据库访问上下文</param>
        public DataOperate(Entities context)
        {
            dbContext = context;
        }

        /// <summary>
        /// 无参数构造
        /// </summary>
        public DataOperate()
        {
            dbContext =new Entities();
        }

        /// <summary>
        /// </summary>
        /// 设置查询前是否启用代理类
        /// <param name="enabled"></param>
        public void SetProxyCreationEnabled(bool enabled)
        {
            dbContext.Configuration.ProxyCreationEnabled = enabled;
        }
        /*
        ~DataOperate()
        {
            dbContext.Dispose();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
        */
        #endregion

        /// <summary>
        /// 提交到数据库
        /// </summary>
        /// <returns></returns>
        public bool SaveChange()
        {
            return dbContext.SaveChanges() > 0;
        }

        public IQueryable<TEntity> FindAll<TEntity>() where TEntity : class
        {
            return dbContext.Set<TEntity>();
        }

        public IQueryable<MEntity> FindAll<TEntity, MEntity>(System.Linq.Expressions.Expression<Func<TEntity, MEntity>> selectProperty) where TEntity : class
        {
            return dbContext.Set<TEntity>().Select(selectProperty);
        }

        /// <summary>
        /// 添加指定实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool Add<TEntity>(TEntity entity) where TEntity : class
        {
            dbContext.Set<TEntity>().Add(entity);
            return SaveChange();
        }

        /// <summary>
        /// 删除指定实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Remove<TEntity>(TEntity entity) where TEntity : class
        {
            dbContext.Set<TEntity>().Remove(entity);
            return SaveChange();
        }

        /// <summary>
        /// 获取指定条件的实体集合
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<TEntity> Filter<TEntity>(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return dbContext.Set<TEntity>().Where<TEntity>(predicate).AsQueryable<TEntity>();
        }

        /// <summary>
        /// 获取指定条件并按指定分页条件进行分页的实体集合
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="total"></param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public IQueryable<TEntity> Filter<TEntity>(Expression<Func<TEntity, bool>> filter, out int total, int index = 0, int size = 50) where TEntity : class
        {
            var skipCount = index * size;
            var resetSet = filter != null
                                ? dbContext.Set<TEntity>().Where<TEntity>(filter).AsQueryable()
                                : dbContext.Set<TEntity>().AsQueryable();
            resetSet = skipCount == 0 ? resetSet.Take(size) : resetSet.Skip(skipCount).Take(size);
            total = resetSet.Count();
            return resetSet.AsQueryable();
        }

        /// <summary>
        /// 系统中是否包含指定条件的实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public bool Contains<TEntity>(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return dbContext.Set<TEntity>().Count<TEntity>(predicate) > 0;
        }

        /// <summary>
        /// 获取指定ID集合的实体集合
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public TEntity Find<TEntity>(params object[] keys) where TEntity : class
        {
            return dbContext.Set<TEntity>().Find(keys);
        }

        /// <summary>
        /// 获取指定条件的实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public TEntity Find<TEntity>(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return dbContext.Set<TEntity>().FirstOrDefault<TEntity>(predicate);
        }

        /// <summary>
        /// 获取指定条件的实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public TEntity Find<TEntity>(Expression<Func<TEntity, TEntity>> selectPropery, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return FindAll<TEntity>().Where(predicate).ToList().FirstOrDefault();
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public bool Remove<TEntity>(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            IQueryable<TEntity> objects = Filter(predicate);
            foreach (var obj in objects)
                dbContext.Set<TEntity>().Remove(obj);
            return dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 更新指定的实体
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Update<TEntity>(TEntity t) where TEntity : class
        {
            try
            {
                DbEntityEntry<TEntity> entry = dbContext.Entry(t);
                dbContext.Set<TEntity>().Attach(t);
                entry.State = EntityState.Modified;
                return SaveChange();
            }
            catch (OptimisticConcurrencyException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取指定条件的实体类型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual TEntity Single<TEntity>(System.Linq.Expressions.Expression<Func<TEntity, bool>> expression) where TEntity : class
        {
            return FindAll<TEntity>().FirstOrDefault(expression);
        }

        /// <summary>
        /// 有条件分页查询
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">显示条数</param>
        /// <param name="filter">条件表达式</param>
        /// <param name="orderBy">排序表达式</param>
        /// <param name="ascending">true：升序，false：降序</param>
        /// <param name="total">总数</param>
        /// <returns></returns>
        public IQueryable<TEntity> GetPaged<TEntity, TKProperty>(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> filterExpression,
            Expression<Func<TEntity, TKProperty>> orderByExpression, bool ascending, out int total) where TEntity : class
        {
            var set = dbContext.Set<TEntity>().Where(filterExpression);
            pageIndex = pageIndex == 0 ? 1 : pageIndex;
            total = set.Count();
            if (ascending)
                return set.OrderBy(orderByExpression).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return set.OrderByDescending(orderByExpression).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }


        /// <summary>
        /// sql 语句查询
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sqlQuery"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters) where TEntity : class
        {
            return dbContext.Database.SqlQuery<TEntity>(sqlQuery, parameters);
        }

        /// <summary>
        /// sql命令执行
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool ExecuteCommand(string sql)
        {
            return dbContext.Database.ExecuteSqlCommand(sql) > 0;
        }

        /// <summary>
        /// 异步执行sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<bool> ExecuteCommandAsync(string sql, params object[] parameters)
        {
            return await dbContext.Database.ExecuteSqlCommandAsync(sql, parameters) > 0;
        }
    }
}
