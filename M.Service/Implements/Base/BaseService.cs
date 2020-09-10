using M.Service.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace M.Service.Implements
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {
        protected Repository.Interfaces.IBaseRepository<TEntity> _baseRepository;
        protected ILogger _logger;
        protected readonly IDistributedCache _cache;
        public BaseService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public virtual async Task<bool> Add(TEntity entity)
        {
            return await _baseRepository.Add(entity);
        }

        public virtual async Task<bool> Add(List<TEntity> entites)
        {
            return await _baseRepository.Add(entites);
        }

        public virtual async Task<bool> Delete(TEntity entity)
        {
            return await _baseRepository.Delete(entity);
        }

        public virtual async Task<bool> Delete(Expression<Func<TEntity, bool>> where)
        {
            return await _baseRepository.Delete(where);
        }

        public virtual async Task<int> ExecuteSqlCommand(string sql)
        {
            return await _baseRepository.ExecuteSqlCommand(sql);
        }

        public virtual async Task<IEnumerable<TEntity>> GetEntities(Expression<Func<TEntity, bool>> where)
        {
            return await _baseRepository.GetEntities(where);
        }

        public virtual async Task<IEnumerable<TEntity>> GetEntitiesForPaging(int page, int pageSize, Expression<Func<TEntity, bool>> where)
        {
            return await _baseRepository.GetEntitiesForPaging(page, pageSize, where);
        }

        public virtual async Task<IEnumerable<TEntity>> GetEntitiesForPaging<TKey>(int page, int pageSize, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> order, bool isAsc = true)
        {
            return await _baseRepository.GetEntitiesForPaging(page, pageSize, where, order);
        }

        public virtual async Task<TEntity> GetEntity(Expression<Func<TEntity, bool>> where)
        {
            return await _baseRepository.GetEntity(where);
        }

        public virtual IEnumerable<dynamic> Query(string sql)
        {
            return _baseRepository.Query(sql);
        }

        public virtual async Task<IEnumerable<TEntity>> Query(string sql, List<SqlParameter> parms)
        {
            return await _baseRepository.Query(sql, parms);
        }

        public virtual async Task<bool> Update(TEntity entity)
        {
            return await _baseRepository.Update(entity);
        }

        public virtual async Task<bool> Update(TEntity entity, params string[] propertyNames)
        {
            return await _baseRepository.Update(entity, propertyNames);
        }
    }
}
