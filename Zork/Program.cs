using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Zork
{
    class Program
    {
        private static void Main(string[] args)
        {
            //Game game = JsonConvert.DeserializeObject<Game>(File.ReadAllText(@"Content\Game.json"));
            
            string roomsFileName = (args.Length > 0) ? args[(int)CommandLineArguments.GameFilename] : @"Content\Game.json";

            Game game = Game.Load(roomsFileName);

            Console.WriteLine("Welcome to Zork!");
            game.Run();
            
            
        }

        //private enum Fields
        //{
        //    Name = 0,
        //    Description 
        //}

        private enum CommandLineArguments
        {
            GameFilename = 0
        }
    }
}
