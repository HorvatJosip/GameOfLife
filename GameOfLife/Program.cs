using System;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new Board(25, 100, 5);
            board.Print();

            while (true)
            {
                Thread.Sleep(200);
                Console.Clear();

                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
                    break;

                board.NextGeneration();
                board.Print();
            }
        }
    }
}
