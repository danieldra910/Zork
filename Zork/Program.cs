using System;

namespace Zork
{
    class Program
    {
        private static void Main(string[] args)
        {
            string roomsFileName = (args.Length > 0) ? args[(int)CommandLineArguments.GameFilename] : @"Content\Game.json";

            Game game = Game.Load(roomsFileName);

            Console.WriteLine("Welcome to Zork!");
            game.Run();
        }

        private enum CommandLineArguments
        {
            GameFilename = 0
        }
    }
}
