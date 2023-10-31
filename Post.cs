using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Parser;

namespace AKRssReader
{
    internal class Post
    {
        private string title;
        private string description;
        private string link;
        private DateTime date;
        private string image;

        public string Title { get => title; set => title = ConvertHttpTextToPlainString(value); }
        public string Description { get => description; set => description = ConvertHttpTextToPlainString(value); }
        public string Link { get => link; set => link = value; }
        public DateTime Date { get => date; set => date = value; }
        public string Image { get => image; set => image = value; }

        public Post()
        {

        }

        private string ConvertHttpTextToPlainString(string httpText)
        {
            string result = "";

            var config = Configuration.Default;

            var context = BrowsingContext.New(config);

            httpText = RemoveImage(httpText);

            string source = httpText;

            var parser = context.GetService<IHtmlParser>();
            var document = parser.ParseDocument(source);

            result = document.DocumentElement.TextContent;

            return result;
        }

        private string RemoveImage(string httpText)
        {
            return Regex.Replace(httpText, "(<img src=\"+[\\s\\S]+\" referrerpolicy=\"no-referrer\">)", "");
        }

        //private string ParseDateTime(string rssDate)
        //{
        //    string result = "";

        //    string parseFormat = "ddd, dd MMM yyyy HH:mm:ss zzz";
        //    DateTime date = DateTime.ParseExact(rssDate, parseFormat, CultureInfo.InvariantCulture);

        //    result = date.ToString("O");

        //    return result;
        //}
    }
}
