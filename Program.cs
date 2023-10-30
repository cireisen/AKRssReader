using static AKRssReader.Logger;


namespace AKRssReader
{
    internal class Program
    {
        
        
        static void Main()
        {
            Console.WriteLine("==========Start Program===========");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");

            StartAsync().GetAwaiter().GetResult();
            
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("==========End Program=============");
        }

        private static async Task StartAsync()
        {
            DBConnector conn = new DBConnector();

            conn.ConnectDB();

            RssReader rssReader = new RssReader();

            await rssReader.OpenRss();
        }
    }
}