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
            Console.WriteLine(divLine);
            Console.WriteLine($"Error Occured While {functionName}");
            Console.WriteLine("");
            Console.WriteLine(e.ToString());
            Console.WriteLine("");
            Console.WriteLine(divLine);
            Console.ResetColor();
        }

        public static void PrintLine(object line)
        {
            Console.WriteLine(line);
            
        }

        public static void PrintWarning(object line)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(divLine);
            Console.WriteLine("");
            Console.WriteLine(line);
            Console.WriteLine("");
            Console.WriteLine(divLine);
            Console.ResetColor();
        }

    }
}
