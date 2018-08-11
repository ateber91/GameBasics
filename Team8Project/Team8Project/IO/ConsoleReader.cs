using System;
using Team8Project.IO.Contracts;

namespace Team8Project.IO
{
    public class ConsoleReader: IReader
    {
        public string ConsoleReadLine()
        {
            var message = Console.ReadLine();
            return message;
        }
        public string ConsoleReadKey()
        {
            string key = Console.ReadKey().KeyChar.ToString();
            return key;
        }
    }
}
