using System;
using System.IO;
using System.Collections.Generic;

namespace Zork
{
    class Program
    {
        private static Room CurrentRoom
        {
            get
            {
                return _rooms[_location.Row, _location.Column];
            }
        }

        private static void Main(string[] args)
        {

            string roomsFileName = (args.Length > 0) ? args[(int)CommandLineArguments.RoomsFilename] : @"Content\Rooms.txt";

            Console.WriteLine("Welcome to Zork!");
            InitializeRoomDescription(roomsFileName);

            Room previousRoom = null;

            bool isRunning = true;

            while (isRunning)
            {
               Console.WriteLine(CurrentRoom);

               if(previousRoom != CurrentRoom)
                {
                    Console.WriteLine(CurrentRoom.Description);
                    previousRoom = CurrentRoom;
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
                        inputString = CurrentRoom.Description;
                        break;
                    case Commands.North:
                    case Commands.South:
                    case Commands.East:
                    case Commands.West:
                        if(Move(command))
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

        private static bool Move(Commands command)
        {
            bool didMove = false;

            switch(command)
            {
                case Commands.North when _location.Row < _rooms.GetLength(0)-1:
                    _location.Row++;
                    didMove = true;
                    break;

                case Commands.South when _location.Row > 0:
                    _location.Row--;
                    didMove = true;
                    break;
                   
                case Commands.East when _location.Column < _rooms.GetLength(1)-1:
                    _location.Column++;
                    didMove = true;
                    break;

                case Commands.West when _location.Column >0:
                    _location.Column--;
                    didMove = true;
                    break;
            }

            return didMove;
        }

        private static readonly Room[,] _rooms = {
            {new Room("Rocky Trail"), new Room("South of House"),new Room("Canyon View") },
            {new Room("Forest"), new Room("West of House"), new Room("Behind House")},
            {new Room("Dense Woods"), new Room("North of House"), new Room("Clearing")}
        };

        private static (int Row, int Column) _location = (1, 1);

        private static void InitializeRoomDescription(string roomsFileName)
        {
            var roomMap = new Dictionary<string, Room>();

            foreach(Room room in _rooms)
            {
                roomMap.Add(room.Name, room);
            }


            string[] lines = File.ReadAllLines(roomsFileName);

            foreach(string line in lines)
            {
                const string fieldDelimiter = "##";
                const int expectedFieldCount = 2;

                string[] fields = line.Split(fieldDelimiter);
                if(fields.Length != expectedFieldCount)
                {
                    throw new ArgumentException("Invalid Record");
                }

                string name = fields[(int)Fields.Name];
                string description = fields[(int)Fields.Description];

                roomMap[name].Description = description;
            }
        }

        private enum Fields
        {
            Name = 0,
            Description 
        }

        private enum CommandLineArguments
        {
            RoomsFilename = 0
        }
    }
}
