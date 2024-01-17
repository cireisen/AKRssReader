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

        public static bool ChangeParameter(string paramNm, string value)
        {
            string jsonString = "";
            using (StreamReader file = File.OpenText(path + @"resource\Config.json"))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {

                    JObject json = (JObject)JToken.ReadFrom(reader);
                    try
                    {
                        json[paramNm] = value;

                        jsonString = json.ToString();
                    }
                    catch (Exception e)
                    {
                        Logger.PrintException(e, "ChangeParameter : ");
                        return false;
                    }
                }
            }


            using (StreamWriter file = new StreamWriter(path + @"resource\Config.json"))
            {
                try
                {
                    file.Write(jsonString);

                    ThreadID = value;

                    file.Close();
                }
                catch (Exception e)
                {
                    Logger.PrintException(e, "ChangeParameter: SaveFile");
                    return false;
                }
            }

            return true;
        }
    }
}
