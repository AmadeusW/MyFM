using MyFm.Core;
using System;
using System.IO;

namespace src
{
    class Program
    {
        static State state;

        static void Main(string[] args)
        {
            var currentPath = Directory.GetCurrentDirectory();
            state = new State();
            state.SetCurrentLocation(currentPath);

            bool running = true;
            do
            {
                running = Repl();
            } while (running);
            Console.WriteLine("Exited. Press Enter to quit");
            Console.ReadLine();
        }

        private static bool Repl()
        {
            var command = Read();
            var result = Evaluate(command);
            Print();
            return result;
        }

        private static void Print()
        {
            Console.SetCursorPosition(0, 2);
            foreach (var location in state.Locations)
            {
                Console.WriteLine(location);
            }
        }

        private static bool Evaluate(string command)
        {
            Console.SetCursorPosition(1, 1);
            if (command.ToLowerInvariant() == "exit")
            {
                Console.WriteLine("Bye");
                return false;
            }
            Console.WriteLine(command);
            return true;
        }

        private static string Read()
        {
            PrintPrompt();
            return Console.ReadLine();
        }

        private static void PrintPrompt()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(state.CurrentLocation.Path);
            Console.Write(" > ");
            Console.SetCursorPosition(state.CurrentLocation.Path.Length + 3, 0);
        }
    }
}
