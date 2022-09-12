using System;


namespace Zork
{
    class Program
    {
        static string CurrentRoom
        {
            get
            {
                return _rooms[_location.Row, _location.Column];
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");
            bool isRunning = true;

            while (isRunning)
            {
                Console.Write($"{CurrentRoom}\n>");
                string inputString = Console.ReadLine().Trim().ToUpper();
                Commands command = ToCommand(inputString);

                string outputString;
                switch (command)
                {
                    case Commands.Quit:
                        isRunning = false;
                        outputString = "Thank you for playing!";
                        break;
                    case Commands.Look:
                        outputString = "This is an open field west of a white house. \nA rubber mat saying 'Welcome to Zork!' lies by the door.";
                        break;
                    case Commands.North:
                    case Commands.South:
                    case Commands.East:
                    case Commands.West:
                        if(Move(command))
                        {
                            outputString = $"You moved {command.ToString()}";
                        }
                        else
                        {
                            outputString = "This way is shut!";
                        }
                        break;
                    default:
                        outputString = "Unknown command";
                        break;
                }
                Console.WriteLine(outputString);
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

        static readonly string[,] _rooms = {
            {"Rocky Trail", "South of House","Canyon View" },
            { "Forest", "West of the House", "Behind the House"},
            {"Dense Woods", "North of House", "Clearing" }
        };

        static (int Row, int Column) _location = (1, 1);
    }
}
