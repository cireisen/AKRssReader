using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRssReader
{
    internal static class DBWorker
    {
        private static string line = Environment.NewLine;

        public static bool SelectExistPost(string link)
        {
            string sql = "SELECT COUNT(*) "
                + line + "  FROM RSS_AK_POSTS"
                + line + " WHERE link = $link";

            Dictionary<string, string> parameter = new Dictionary<string, string>();
            parameter.Add("$link", link);

            using (DBConnector conn = new DBConnector()) 
            {
                conn.ConnectDB();
                using (var result = conn.ExecuteQuery(sql, parameter))
                {
                    if (result == null)
                    {
                        return true;
                    }
                    else
                    {
                        if (result.Read())
                        {
                            if (result.GetInt32(0) > 0)
                            {
                                result.Close();
                                return true;
                            }
                            else
                            {
                                result.Close();
                                return false;
                            }
                        }
                        else
                        {
                            result.Close();
                            return true;
                        }
                    }
                }
            }
        }

        public static bool InsertPost(Post post)
        {
            string sql = "INSERT INTO RSS_AK_POSTS(link, title,description, uploadtime)"
                + line + "                  VALUES($link, $title, $description, $uploadtime)";

            Dictionary<string, string> parameter = new Dictionary<string, string>();
            parameter.Add("$link", post.Link);
            parameter.Add("$title", post.Title);
            parameter.Add("$description", post.Description);
            parameter.Add("$uploadtime", post.Date.ToString());

            using (DBConnector conn = new DBConnector())
            {
                conn.ConnectDB();
                return conn.ExecuteNonQuery(sql, parameter);
            }

        }
    }
}
