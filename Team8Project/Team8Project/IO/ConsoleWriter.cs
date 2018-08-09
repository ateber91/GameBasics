using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team8Project.Contracts;

namespace Team8Project.IO
{
    public class ConsoleWriter : IWriter
    {
        public void ConsoleWriteLine(string message)
        {
            Console.WriteLine(message);
        }
        public void ConsoleWrite(string message)
        {
            Console.Write(message);
        }
    }
}
