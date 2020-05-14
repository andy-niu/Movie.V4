using M.Repository.Context;
using M.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace M.Repository.Implements
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        private readonly IDbContextFactory _dbContextFactory;
        public BaseRepository(IDbContextFactory _dbContextFactory)
        {
            this._dbContextFactory = _dbContextFactory;
        }

        protected MovieBaseDbContext GetMovieDbContext()
        {
            return _dbContextFactory.GetMovieDBContext();
        }

        [Obsolete]
        public async Task<int> ExecuteSqlCommand(string sql)
        {
            return await GetMovieDbContext().Database.ExecuteSqlCommandAsync(sql);
        }

        [Obsolete]
        public IEnumerable<dynamic> Query(string sql)
        {
            var _db = GetMovieDbContext();
            using (var cmd = _db.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = sql;

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var row = new System.Dynamic.ExpandoObject() as IDictionary<string, object>;
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        row.Add(reader.GetName(i), reader[i]);
                    }
                    yield return row;
                }
            }
        }

        [Obsolete]
        public async Task<IEnumerable<TEntity>> Query(string sql, List<SqlParameter> parms, CommandType cmdType = CommandType.Text)
        {
            var _db = GetMovieDbContext();
            //存储过程（exec getActionUrlId @name,@ID）
            if (cmdType == CommandType.StoredProcedure)
            {
                StringBuilder paraNames = new StringBuilder();
                foreach (var sqlPara in parms)
                {
                    paraNames.Append($" @{sqlPara},");
                }
                sql = paraNames.Length > 0 ? $"exec {sql} {paraNames.ToString().Trim(',')}" : $"exec {sql} ";
            }

            return await _db.Set<TEntity>().FromSql(sql).ToArrayAsync();

        }

        public async Task<bool> Add(TEntity entity)
        {
            var _db = GetMovieDbContext();
            await _db.Set<TEntity>().AddAsync(entity);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> Add(List<TEntity> Entites)
        {
            var _db = GetMovieDbContext();
            await _db.Set<TEntity>().AddRangeAsync(Entites);
            return await _db.SaveChangesAsync() == Entites.Count;
        }

        public async Task<bool> Delete(TEntity Entity)
        {
            var _db = GetMovieDbContext();
            _db.Set<TEntity>().Remove(Entity);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Expression<Func<TEntity, bool>> exp)
        {
            var _db = GetMovieDbContext();
            var array = _db.Set<TEntity>().Where(exp).ToList();
            array.ForEach(item =>
            {
                _db.Set<TEntity>().Attach(item); 
                _db.Set<TEntity>().Remove(item);
            });
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(TEntity Entity)
        {
            var _db = GetMovieDbContext();
            _db.Set<TEntity>().Update(Entity);
            return await _db.SaveChangesAsync() > 0;
        }
        public async Task<TEntity> GetEntity(Expression<Func<TEntity, bool>> where)
        {
            var _db = GetMovieDbContext();
            return await _db.Set<TEntity>().Where(where).AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<TEntity>> GetEntities(Expression<Func<TEntity, bool>> where)
        {
            var _db = GetMovieDbContext();
            return await _db.Set<TEntity>().Where(where).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetEntitiesForPaging(int Page, int pageSize, Expression<Func<TEntity, bool>> where)
        {
            var _db = GetMovieDbContext();
            return await _db.Set<TEntity>().Where(where).Skip((Page - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetEntitiesForPaging<TKey>(int page, int pageSize, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> order, bool isAsc = true)
        {
            var _db = GetMovieDbContext();
            if (isAsc)
            {
                return await _db.Set<TEntity>().Where(where).OrderBy(order).Skip((page - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
            }
            else
            {
                return await _db.Set<TEntity>().Where(where).OrderByDescending(order).Skip((page - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
            }
        }

        public async Task<bool> Update(TEntity entity, params string[] propertyNames)
        {
            var _db = GetMovieDbContext();
            //3.1.1 将对象添加到EF中
            EntityEntry entry = _db.Entry<TEntity>(entity);
            //3.1.2 先设置对象的包装状态为 Unchanged
            entry.State = EntityState.Unchanged;
            //3.1.3 循环被修改的属性名数组
            foreach (string propertyName in propertyNames)
            {
                //将每个被修改的属性的状态设置为已修改状态；这样在后面生成的修改语句时，就只为标识为已修改的属性更新
                entry.Property(propertyName).IsModified = true;
            }
            return await _db.SaveChangesAsync() > 0;
        }
    }


}
