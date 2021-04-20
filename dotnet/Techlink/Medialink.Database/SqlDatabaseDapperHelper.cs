using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Medialink.Database
{
    public class SqlDatabaseDapperHelper
    {
        private readonly string _sqlConnectionString;

        public SqlDatabaseDapperHelper(string server, string username, string password, string database)
        {
            _sqlConnectionString = $@"server='{server}';  user id='{username}';  password='{password}';  database='{database}';";
        }

        public SqlDatabaseDapperHelper(string connectionString)
        {
            _sqlConnectionString = connectionString;
        }

        /// <summary>
        /// Execute parameterized SQL that selects a result set.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">Your SQL select statement.</param>
        /// <param name="parameters">Usage: var parameters = new { Param1=your_value_1, Param2=your_value_2, . . . };</param>
        /// <returns></returns>
        public List<T> Query<T>(string query, object parameters)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(_sqlConnectionString))
                    return conn.Query<T>(query, parameters).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Execute parameterized SQL that returns the number of rows affected.
        /// </summary>
        /// <param name="query">Your SQL statement.</param>
        /// <param name="parameters">Usage: var parameters = new { Param1=your_value_1, Param2=your_value_2, . . . };</param>
        /// <returns></returns>
        public int Execute(string query, object parameters)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(_sqlConnectionString))
                    return conn.Execute(query, parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Execute parameterized SQL that selects a singe value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">Your SQL select statement.</param>
        /// <param name="parameters">Usage: var parameters = new { Param1=your_value_1, Param2=your_value_2, . . . };</param>
        /// <returns></returns>
        public T ExecuteScalar<T>(string query, object parameters)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(_sqlConnectionString))
                    return conn.ExecuteScalar<T>(query, parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
