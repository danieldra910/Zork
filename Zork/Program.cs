﻿using System;
using System.Collections.Generic;

namespace Zork
{
    class Program
    {
        static Room CurrentRoom
        {
            get
            {
                return _rooms[_location.Row, _location.Column];
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");
            InitializeRoomDescription();

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

        static Commands ToCommand(string commandString)
        {
            return Enum.TryParse(commandString, true, out Commands result) ? result : Commands.Unknown;
        }

        static bool Move(Commands command)
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

        static readonly Room[,] _rooms = {
            {new Room("Rocky Trail"), new Room("South of House"),new Room("Canyon View") },
            {new Room("Forest"), new Room("West of the House"), new Room("Behind the House")},
            {new Room("Dense Woods"), new Room("North of House"), new Room("Clearing")}
        };

        static (int Row, int Column) _location = (1, 1);

        static void InitializeRoomDescription()
        {

            Dictionary<string, Room> roomMap = new Dictionary<string, Room>();
            foreach (Room room in _rooms)
            {
                roomMap.Add(room.Name, room);
            }

            roomMap["Rocky Trail"].Description = "You are on a rock-strewn trail.";
            roomMap["South of House"].Description = "You are facing the south side of a white house. There is no door here, and all the windows are barred.";
            roomMap["Canyon View"].Description = "You are at the top of the Great Canyon on its south wall.";

            roomMap["Forest"].Description = "This is a forest, with trees in all directions around you.";
            roomMap["West of the House"].Description = "This is an open field west of a white house, with a boarded front door.";
            roomMap["Behind the House"].Description = "You are behind the white house. In one corner of the house there is a small window which is slightly ajar.";

            roomMap["Dense Woods"].Description = "This is a dimly lit forest, withh large trees all around. To the east, there appears to be sunlight.";
            roomMap["North of House"].Description = "You are facing the north side of a white house. There is no door here, and all the windows are barred.";
            roomMap["Clearing"].Description = "You are in a clearing, with a forest surrounding you on the west and south.";
        }
    }
}
