using System;
using Team8Project.IO.Contracts;

namespace Team8Project.IO
{
    public class ConsoleWriter : IWriter
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
        public void ConsoleWrite(string message)
        {
            Console.Write(message);
        }
        public void ConsoleClear()
        {
            Console.Clear();
        }
        public void PrintOnPosition(int row, int col, string message, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.SetCursorPosition(col, row);
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
