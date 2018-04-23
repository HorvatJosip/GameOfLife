using System;

namespace GameOfLife
{
    static class Utils
    {
        public static void PrintInColor(string data, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(data);
            Console.ResetColor();
        }

        public static void PrintInColor(char data, ConsoleColor color) => PrintInColor(data.ToString(), color);
    }
}
