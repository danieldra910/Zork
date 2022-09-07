using System;


namespace Zork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");
            bool isRunning = true;

            while (isRunning)
            {
                Console.Write($"{_rooms[_currentRoom]}\n>");
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
                case Commands.North:
                case Commands.South:
                    didMove = false;
                    break;
                
                case Commands.East when _currentRoom < _rooms.Length-1:
                    _currentRoom++;
                    didMove = true;
                    break;

                case Commands.West when _currentRoom >0:
                    _currentRoom--;
                    didMove = true;
                    break;
            }

            return didMove;
        }

        static readonly string[] _rooms = { "Forest", "West of the House","Behind the House","Clearing","Canyon View"};
        static int _currentRoom = 1;

    }
}
