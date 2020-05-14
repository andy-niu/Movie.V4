using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace M.Repository.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {

        Task<int> ExecuteSqlCommand(string sql);

        IEnumerable<dynamic> Query(string sql);

        Task<IEnumerable<TEntity>> Query(string sql, List<SqlParameter> parms, CommandType cmdType = CommandType.Text);

        Task<bool> Add(TEntity entity) ;

        Task<bool> Add(List<TEntity> entites) ;

        Task<bool> Delete(TEntity entity);

        Task<bool> Delete(Expression<Func<TEntity, bool>> where) ;

        Task<bool> Update(TEntity entity) ;
        Task<bool> Update(TEntity entity, params string[] propertyNames);

        Task<TEntity> GetEntity(Expression<Func<TEntity, bool>> where);

        Task<IEnumerable<TEntity>> GetEntities(Expression<Func<TEntity, bool>> where) ;

        Task<IEnumerable<TEntity>> GetEntitiesForPaging(int page, int pageSize, Expression<Func<TEntity, bool>> where);

        Task<IEnumerable<TEntity>> GetEntitiesForPaging<TKey>(int page, int pageSize, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> order, bool isAsc = true);

    }
}
