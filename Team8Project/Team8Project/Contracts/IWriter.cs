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
        void PrintOnPosition(int row, int col, string message, ConsoleColor color = ConsoleColor.Gray);
    }
}
