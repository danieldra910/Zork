using Newtonsoft.Json;
using System;
using System.IO;

namespace Zork
{
    public class Game
    {
        public World World { get; private set; }

        [JsonIgnore]
        public Player Player { get; private set; }

        public Game (World world, Player player)
        {
            World = world;
            Player = player;
        }

        public void Run()
        {
            Room previousRoom = null;

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine(Player.Location);

                if (previousRoom != Player.Location)
                {
                    Console.WriteLine(Player.Location.Description);
                    previousRoom = Player.Location;
                }
                Console.Write("> ");

                string inputString = Console.ReadLine().Trim();
                Commands command = ToCommand(inputString);

                switch (command)
                {
                    case Commands.Quit:
                        isRunning = false;
                        inputString = "Thank you for playing!";
                        break;
                    case Commands.Look:
                        inputString = Player.Location.Description;
                        break;
                    case Commands.North:
                    case Commands.South:
                    case Commands.East:
                    case Commands.West:
                        Directions direction = Enum.Parse<Directions>(command.ToString(), true);
                        if (Player.Move(direction))
                        {
                            inputString = $"You moved {command.ToString()}";
                        }
                        else
                        {
                            inputString = "This way is shut!";
                        }
                        break;
                    default:
                        inputString = "Unknown command";
                        break;
                }
                Console.WriteLine(inputString);
            }
        }
        private static Commands ToCommand(string commandString)
        {
            return Enum.TryParse(commandString, true, out Commands result) ? result : Commands.Unknown;
        }

        public static Game Load(string filename)
        {
            Game game = JsonConvert.DeserializeObject<Game>(File.ReadAllText(filename));
            game.Player = game.World.SpawnPlayer();

            return game;
        }
    }
}
