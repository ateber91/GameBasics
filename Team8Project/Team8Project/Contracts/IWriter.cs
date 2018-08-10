using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team8Project.Contracts
{
    public interface IWriter
    {
        void ConsoleWriteLine(string message);
        void ConsoleWrite(string message);
        void ConsoleClear();
    }
}
