using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AKRssReader
{
    internal static class WebHookExecuter
    {
        public static async Task SendRssDataToDiscord(Post post)
        {
            string url = Config.WebHookLink;

            //thread 여부 확인
            if(!string.IsNullOrEmpty(Config.ThreadID))
            {
                url += @"?thread_id=" + Config.ThreadID;
            }

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "POST";
            webRequest.Timeout = 30 * 1000;
            webRequest.ContentType = "application/json; charset=utf-8";
            
            var json = new JObject();
            json.Add("content", "");
            json.Add("tts", false);
            json.Add("embeds", CreatePostJArray(post));

            byte[] byteArray = Encoding.UTF8.GetBytes(json.ToString());

            Stream dataStream = webRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            dataStream.Dispose();
            using (HttpWebResponse resp = (HttpWebResponse)webRequest.GetResponse())
            {
                HttpStatusCode status = resp.StatusCode;
                Console.WriteLine(status);

                // 응답과 관련된 stream을 가져온다.
                Stream respStream = resp.GetResponseStream();
                using (StreamReader streamReader = new StreamReader(respStream))
                {
                    var responseText = streamReader.ReadToEnd();
                }
            }
        }

        private static JArray CreatePostJArray(Post post)
        {
            JArray postArray = new JArray();

            var json = new JObject();

            json.Add("title", post.Title);
            json.Add("description", post.Description);
            json.Add("color", 2326507);
            
            var author = new JObject();
            author.Add("name", "明日方舟");
            json.Add("author", author);
            
            var image = new JObject();
            image.Add("url", post.Image);
            json.Add("image", image);

            json.Add("url", post.Link);
            json.Add("timestamp", post.Date);

            postArray.Add(json);

            return postArray;
        }
    }
}
