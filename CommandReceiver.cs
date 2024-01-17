using AngleSharp.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRssReader
{
    static public class CommandReceiver
    {
        static public async void ReadLines()
        {
            while(true)
            {
                string line = Console.ReadLine();

                var split = line.SplitSpaces();

                List<string> param = split.ToList();
                param.RemoveAt(0);

                CallCommand(split[0], param.ToArray());
            }
        }

        static public void CallCommand(string command, params string[] parameters)
        {
            switch(command)
            {
                case "changethread":
                    ChangeThread(parameters[0]);
                    break;
            }
        }

        static private void ChangeThread(string param)
        {
            bool result = Config.ChangeParameter("ThreadID", param);

            if(result)
            {
                Logger.PrintLine($"스레드가 '{param}'으로 변경이 성공적으로 완료되었습니다.");
            }
            else
            {
                Logger.PrintWarning("스레드 변경중 에러가 발생하였습니다.");
            }
        }
    }
}
