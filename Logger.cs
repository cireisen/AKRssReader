using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRssReader
{
    static class Logger
    {
        static private string divLine = "============================================================";

        public static void PrintException(Exception e, string functionName)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            PrintLine(divLine);
            PrintLine($"Error Occured While {functionName}");
            PrintLine("");
            PrintLine(e.ToString());
            PrintLine("");
            PrintLine(divLine);
            Console.ResetColor();
        }

        public static void PrintLine(object line)
        {
            Console.WriteLine($"AKRssReader {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} : {line}");
            
        }

        public static void PrintWarning(object line)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintLine(divLine);
            PrintLine("");
            PrintLine(line);
            PrintLine("");
            PrintLine(divLine);
            Console.ResetColor();
        }

    }
}
