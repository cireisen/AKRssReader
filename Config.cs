using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRssReader
{
    internal static  class Config
    {
        public static readonly string path = AppDomain.CurrentDomain.BaseDirectory + "\\";
        public static string WebHookLink = "";
        public static string ThreadID = "";
        public static void InitConfig()
        {
            using (StreamReader file = File.OpenText(path + @"resource\Config.json"))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {

                    JObject json = (JObject)JToken.ReadFrom(reader);
                    try
                    {
                        WebHookLink = json["WebHookLink"].ToString();
                        ThreadID = json["ThreadID"].ToString();
                    }
                    catch (Exception e)
                    {
                        Logger.PrintException(e, "InitConfig");
                    }
                }
            }
        }
    }
}
