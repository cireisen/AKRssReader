using static AKRssReader.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Xml;
using System.ServiceModel.Syndication;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace AKRssReader
{
    internal class RssReader
    {
        string url = @"https://rsshub.app/bilibili/user/dynamic/161775300";

        public async Task OpenRss()
        {
            PrintLine("Connect To AKRss");
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();

            PrintLine("Complete Read Rss");

            List<Post> postList = new List<Post>();
            foreach(var item in feed.Items)
            {
                Post post = new Post();
                post.Title = item.Title.Text;
                post.Description = item.Summary.Text;
                post.Image = GetFirstImage(item.Summary.Text);
                post.Link = item.Id;
                post.Date = item.PublishDate.Date;
                postList.Add(post);
            }

            postList.Reverse();

            foreach(Post post in postList)
            {
                if (DBWorker.SelectExistPost(post.Link))
                {
                    continue;
                }
                await WebHookExecuter.SendRssDataToDiscord(post);
                DBWorker.InsertPost(post);
            }
        }

        private string GetFirstImage(string htmlText)
        {
            string result = "";

            var images = Regex.Matches(htmlText, "(?<=<img src=\")(.*?)(?=\" referrerpolicy=\"no-referrer\">)");

            if(images.Count > 0)
            {
                result = images[0].Value;
            }

            return result;
        }
    }
}
