using static AKRssReader.Logger;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRssReader
{
    internal class DBConnector
    {
        private string line = Environment.NewLine;
        private string _connectionString = "";
        private SqliteConnection _conn;
        public DBConnector() 
        {
            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_sqlite3());

            _connectionString = new SqliteConnectionStringBuilder("Data Source=RSSListdb.db;")
            {
                Mode = SqliteOpenMode.ReadWriteCreate
            }.ToString();
        }

        public bool ConnectDB()
        {
            try
            {
                _conn= new SqliteConnection(_connectionString);
                
                _conn.Open();

                CheckTableExist();
                
                return true;
            }
            catch (Exception e) 
            {
                PrintException(e, "ConnectDB");
                _conn.Close();
                return false;
            }
        }

        public SqliteDataReader? ExecuteQuery(string sql)
        {
            try
            {
                var command = _conn.CreateCommand();

                command.CommandText = sql;

                return command.ExecuteReader();
            }
            catch(Exception e) 
            {
                PrintException(e, "ExecuteQuery");
                return null;
            }
        }

        public SqliteDataReader? ExecuteQuery(string sql, Dictionary<string, string> parameters)
        {
            try
            {
                var command = _conn.CreateCommand();

                command.CommandText = sql;

                foreach(var parameter in parameters)
                {
                    SqliteParameter param = new SqliteParameter()
                    {
                        ParameterName = parameter.Key,
                        Value = parameter.Value,
                    };

                    command.Parameters.Add(param);
                }

                return command.ExecuteReader();
            }
            catch(Exception e)
            {
                PrintException(e, "ExecuteQueryWithParam");
                return null;
            }
        }

        public bool ExecuteNonQuery(string sql)
        {
            bool result = false;

            var command = _conn.CreateCommand();

            command.CommandText = sql;

            int resultCode = command.ExecuteNonQuery();

            if (resultCode != -1)
            {
                result = true;
            }
            return result;
        }

        public bool ExecuteNonQuery(string sql, Dictionary<string, string> parameters)
        {
            bool result = false;

            var command = _conn.CreateCommand();

            command.CommandText = sql;

            foreach (var parameter in parameters)
            {
                SqliteParameter param = new SqliteParameter()
                {
                    ParameterName = parameter.Key,
                    Value = parameter.Value,
                };

                command.Parameters.Add(param);
            }

            int resultCode = command.ExecuteNonQuery();

            if (resultCode != -1)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 테이블이 존재하는지 확인 없으면 생성
        /// </summary>
        /// <returns></returns>
        private void CheckTableExist()
        {
            string sql = "SELECT * FROM sqlite_master WHERE type='table';";

            SqliteDataReader reader = ExecuteQuery(sql);

            _ = reader;

            if(reader == null)
            {
                sql = "CREATE TABLE RSS_AK_POSTS(seq int, link text, title text, description text, uploadtime text)";

                if(!ExecuteNonQuery(sql))
                {
                    PrintWarning("while CreateTable executed -1");
                }
            }
        }
    }
}
