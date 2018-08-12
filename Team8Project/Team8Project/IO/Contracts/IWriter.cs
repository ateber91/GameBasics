using System;

namespace Team8Project.IO.Contracts
{
    public interface IWriter
    {
        void WriteLine(string message);
        void ConsoleWrite(string message);
        void ConsoleClear();
        void PrintOnPosition(int row, int col, string message, ConsoleColor color = ConsoleColor.Gray);
    }
}
