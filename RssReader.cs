using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Xml;
using System.ServiceModel.Syndication;
using System.Xml.Linq;

namespace AKRssReader
{
    internal class RssReader
    {
        string url = @"https://rsshub.app/bilibili/user/dynamic/161775300";

        public async Task OpenRss()
        {
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();
            foreach(var item in feed.Items)
            {

            }
        }
    }
}
