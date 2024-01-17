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

            PrintLine("실행 전에 스레드를 변경하시겠습니까?: Y/N");
            if(Console.ReadLine() == "Y")
            {
                PrintLine("변경할 스레드ID를 입력해 주세요.");
                string threadID = Console.ReadLine();

                CommandReceiver.CallCommand("changethread", threadID);
            }

            Thread readLine = new Thread(new ThreadStart(CommandReceiver.ReadLines));
            readLine.Start();

            while (true)
            {
                RssReader rssReader = new RssReader();

                PrintLine("Start ReadRss");
                await rssReader.OpenRss();  

                await Task.Delay(1000 * 60 * 30);
            }
        }
    }
}