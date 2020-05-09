using M.Repository.Context;
using M.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace M.Repository.Implement
{
    public class BaseRepository : IBaseRepository
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
        public int ExecuteSqlCommand(string sql)
        {
            return GetMovieDbContext().Database.ExecuteSqlCommand(sql);
        }

        public IEnumerable<dynamic> SqlQuery(string sql)
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

        public async Task<bool> Add<T>(T Entity) where T : class
        {
            var _db = GetMovieDbContext();
            await _db.Set<T>().AddAsync(Entity);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete<T>(T Entity) where T : class
        {
            var _db = GetMovieDbContext();
            _db.Set<T>().Remove(Entity);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete<T>(Expression<Func<T, bool>> exp) where T : class
        {
            var _db = GetMovieDbContext();
            var func = EF.CompileQuery((MovieBaseDbContext context, Expression<Func<T, bool>> exps) => context.Set<T>().Where(exp));
            var result = func(_db, exp);
            foreach (var item in result)
            {
                _db.Set<T>().Remove(item);
            }
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update<T>(T Entity) where T : class
        {
            var _db = GetMovieDbContext();
            _db.Set<T>().Update(Entity);
            return await _db.SaveChangesAsync() > 0;
        }

        public IEnumerable<T> GetEntities<T>(Expression<Func<T, bool>> exp) where T : class
        {
            return CompileQuery<T>(exp);
        }

        public IEnumerable<T> GetEntitiesForPaging<T>(int Page, int pageSize, Expression<Func<T, bool>> exp) where T : class
        {
            return CompileQuery(exp).Skip((Page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<T> GetEntitiesForPaging<T>(int Page, int pageSize, Expression<Func<T, bool>> exp, Expression<Func<T, object>> orderBy) where T : class
        {
            return CompileQuery(exp, orderBy).Skip((Page - 1) * pageSize).Take(pageSize);
        }


        public T GetEntity<T>(Expression<Func<T, bool>> exp) where T : class
        {
            return CompileQuerySingle(exp);
        }

        private IEnumerable<T> CompileQuery<T>(Expression<Func<T, bool>> exp) where T : class
        {
            var _db = GetMovieDbContext();
            var func = EF.CompileQuery((MovieBaseDbContext context, Expression<Func<T, bool>> exps) => context.Set<T>().Where(exp));
            return func(_db, exp);
        }
        private IEnumerable<T> CompileQuery<T>(Expression<Func<T, bool>> exp, Expression<Func<T, object>> orderBy) where T : class
        {
            var _db = GetMovieDbContext();
            var func = EF.CompileQuery((MovieBaseDbContext context, Expression<Func<T, bool>> exps) => context.Set<T>().Where(exp).OrderBy(orderBy));
            return func(_db, exp);
        }

        private T CompileQuerySingle<T>(Expression<Func<T, bool>> exp) where T : class
        {
            var _db = GetMovieDbContext();
            var func = EF.CompileQuery((MovieBaseDbContext context, Expression<Func<T, bool>> exps) => context.Set<T>().FirstOrDefault(exp));
            return func(_db, exp);
        }
    }


}
