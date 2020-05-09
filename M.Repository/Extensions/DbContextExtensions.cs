using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace M.Repository.Extensions
{
    public static class DbContextExtensions
    {
        public static T ExecuteQueryModel<T>(this DatabaseFacade facade, string sql, params SqlParameter[] parameters)
            where T : class, new()
        {
            IEnumerable<T> result = ExecuteQuery<T>(facade, sql, parameters);
            if (result != null && result.Any() && result.Count() == 1)
            {
                return result.First();
            }
            return null;
        }

        public static IEnumerable<T> ExecuteQuery<T>(this DatabaseFacade facade, string sql, params SqlParameter[] parameters)
            where T : class, new()
        {
            DataTable dt = ExecuteDataTable(facade, sql, parameters);
            return dt.ToEnumerable<T>();
        }

        public static DataTable ExecuteDataTable(this DatabaseFacade facade, string sql, params SqlParameter[] parameters)
        {
            using (var reader = facade.ExecuteReader(sql, parameters))
            {
                DataTable dt = new DataTable();
                dt.Load(reader);
                return dt;
            }
        }

        public static DbDataReader ExecuteReaderSingleRow(this DatabaseFacade facade, string sql, params SqlParameter[] parameters)
        {
            return facade.ExecuteReader(sql, CommandBehavior.SingleRow, parameters);
        }

        public static DbDataReader ExecuteReader(this DatabaseFacade facade, string sql, params SqlParameter[] parameters)
        {
            return facade.ExecuteReader(sql, CommandBehavior.Default, parameters);
        }

        public static object ExecuteScalar(this DatabaseFacade facade, string sql, params SqlParameter[] parameters)
        {
            return facade.ExecuteScalar(null, sql, parameters);
        }
        public static object ExecuteScalar(this DatabaseFacade facade, DbTransaction trans, string sql, params SqlParameter[] parameters)
        {
            using (var cmd = facade.GetDbConnection().CreateCommand())
            {
                ParamaterHandle(parameters);
                if (trans != null)
                {
                    cmd.Transaction = trans;
                }
                cmd.CommandText = sql;
                if (parameters != null && parameters.Any())
                {
                    cmd.Parameters.AddRange(parameters);
                }
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }
                return cmd.ExecuteScalar();
            }
        }

        public static int ExecuteSqlCommand(this DatabaseFacade facade, string sql, params SqlParameter[] parameters)
        {
            return facade.ExecuteSqlCommand(null, sql, parameters);
        }

        public static int ExecuteSqlCommand(this DatabaseFacade facade, DbTransaction trans, string sql, params SqlParameter[] parameters)
        {
            using (var cmd = facade.GetDbConnection().CreateCommand())
            {
                ParamaterHandle(parameters);
                if (trans != null)
                {
                    cmd.Transaction = trans;
                }
                cmd.CommandText = sql;
                if (parameters != null && parameters.Any())
                {
                    cmd.Parameters.AddRange(parameters);
                }
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }
                return cmd.ExecuteNonQuery();
            }
        }


        #region Async

        public async static Task<int> ExecuteSqlCommandAsync(this DatabaseFacade facade, string sql, params SqlParameter[] parameters)
        {
            return await facade.ExecuteSqlCommandAsync(null, sql, parameters);
        }

        public async static Task<int> ExecuteSqlCommandAsync(this DatabaseFacade facade, DbTransaction trans, string sql, params SqlParameter[] parameters)
        {
            using (var cmd = facade.GetDbConnection().CreateCommand())
            {
                ParamaterHandle(parameters);
                if (trans != null)
                {
                    cmd.Transaction = trans;
                }
                cmd.CommandText = sql;
                if (parameters != null && parameters.Any())
                {
                    cmd.Parameters.AddRange(parameters);
                }
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }
                return await cmd.ExecuteNonQueryAsync();
            }
        }

        #endregion

        #region Private Methods
        private static DbDataReader ExecuteReader(this DatabaseFacade facade, string sql, CommandBehavior behavior = CommandBehavior.Default, params SqlParameter[] parameters)
        {
            DbDataReader reader = null;
            using (var cmd = facade.GetDbConnection().CreateCommand())
            {
                ParamaterHandle(parameters);
                cmd.CommandText = sql;
                if (parameters != null && parameters.Any())
                {
                    cmd.Parameters.AddRange(parameters);
                }
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                if (behavior == CommandBehavior.Default)
                {
                    reader = cmd.ExecuteReader();
                }
                else
                {
                    reader = cmd.ExecuteReader(behavior);
                }
            }
            return reader;
        }
        private static IEnumerable<T> ToEnumerable<T>(this DataTable dt) where T : class, new()
        {
            PropertyInfo[] propertyInfos = typeof(T).GetProperties();
            T[] ts = new T[dt.Rows.Count];
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                T t = new T();
                foreach (PropertyInfo p in propertyInfos)
                {
                    if (dt.Columns.IndexOf(p.Name) != -1 && row[p.Name] != DBNull.Value)
                        p.SetValue(t, row[p.Name], null);
                }
                ts[i] = t;
                i++;
            }
            return ts;
        }

        private static void ParamaterHandle(SqlParameter[] parameters)
        {
            if (parameters != null && parameters.Any())
            {
                foreach (var param in parameters)
                {
                    if (param.Value == null)
                    {
                        param.Value = DBNull.Value;
                    }
                }
            }
        }
        #endregion
    }

}
