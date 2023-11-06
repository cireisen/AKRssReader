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

            PrintLine("Get ConfigFile Data");
            Config.InitConfig();

            StartAsync().GetAwaiter().GetResult();
            
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("==========End Program=============");
        }

        private static async Task StartAsync()
        {
            DBConnector conn = new DBConnector();
            
            PrintLine("Connecting to DB");
            conn.ConnectDB();

            PrintLine("Connect Complete");

            while(true)
            {
                RssReader rssReader = new RssReader();

                PrintLine("Start ReadRss");
                await rssReader.OpenRss();

                await Task.Delay(1000 * 60 * 30);
            }
        }
    }
}