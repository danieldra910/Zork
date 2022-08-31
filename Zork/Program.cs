﻿using System;


namespace Zork
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Welcome to Zork!");
            string inputString = Console.ReadLine().Trim().ToUpper();
            Commands command = ToCommand(inputString);
            Console.WriteLine(command);

            if(command == Commands.Quit)
            {
                Console.WriteLine("Thank you for playing.");
            }

            else if(command == Commands.Look)
            {
                Console.WriteLine("This is an open field west of a white house, with a boarded front door.\nA rubber mat saying 'Welcome to Zork!' lies by the door.");
            }

            else if( command == Commands.North)
            {
                Console.WriteLine("You moved North");
            }

            else if (command == Commands.South)
            {
                Console.WriteLine("You moved South");
            }

            else if (command == Commands.East)
            {
                Console.WriteLine("You moved East");
            }

            else if (command == Commands.West)
            {
                Console.WriteLine("You moved West");
            }

            else
            {
                Console.WriteLine($"Unknown command: {inputString}");
            }

        }

        static Commands ToCommand(string commandString)
        {
            return Enum.TryParse(commandString, true, out Commands result) ? result : Commands.Unknown;
        }

    }
}
