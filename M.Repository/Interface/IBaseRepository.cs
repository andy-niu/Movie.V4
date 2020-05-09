using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace M.Repository.Interface
{
    public interface IBaseRepository
    {

        int ExecuteSqlCommand(string sql);

        IEnumerable<dynamic> SqlQuery(string sql);

        Task<bool> Add<T>(T Entity) where T : class;

        Task<bool> Delete<T>(T Entity) where T : class;

        Task<bool> Delete<T>(Expression<Func<T, bool>> exp) where T : class;

        Task<bool> Update<T>(T Entity) where T : class;

        IEnumerable<T> GetEntities<T>(Expression<Func<T, bool>> exp) where T : class;

        IEnumerable<T> GetEntitiesForPaging<T>(int Page, int pageSize, Expression<Func<T, bool>> exp) where T : class;

        T GetEntity<T>(Expression<Func<T, bool>> exp) where T : class;
    }
}
