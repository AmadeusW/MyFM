using MyFm.Core;
using MyFm.Core.Commands;
using MyFm.Core.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyFm.Cli
{
    class Program
    {
        public static Dictionary<string, ICommand> InvocationTable { get; private set; }

        static void Main(string[] args)
        {
            var currentPath = Directory.GetCurrentDirectory();
            var state = new State(); 
            state.SetCurrentLocation(currentPath);
            InvocationTable = InitializeCommands();

            do
            {
                state = Repl(state);
            } while (state.Running);
            Console.WriteLine("Exited. Press Enter to quit");
            Console.ReadLine();
        }

        private static Dictionary<string, ICommand> InitializeCommands()
        {
            var knownCommands = new List<ICommand>
            {
                new ChangeDirectoryCommand(),
                new ExitCommand(),
                new SetPathCommand(),
            };

            var invocationTable = knownCommands.ToDictionary(n => n.Name, n => n);
            return invocationTable;
        }

        private static State Repl(State state)
        {
            try
            {
                PrintPrompt(state);
                var instruction = Read(state);
                var newState = instruction.Item1.Evaluate(state, instruction.Item2);
                Print(newState);
                return newState;
            }
            catch (Exception ex)
            {
                Console.SetCursorPosition(0, 9);
                Console.WriteLine(ex.Message);
                return state;
            }
        }

        private static void PrintPrompt(State state)
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(state.CurrentLocation.Path);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("> ");
            Console.Write("                                                               "); // TODO: find a better way
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(state.CurrentLocation.Path.Length + 2, 0);
        }

        private static (ICommand, string) Read(State state)
        {
            string query = new KeyProcessor().ReadCommand(state).Trim();
            var wordBoundary = query.IndexOf(' ');
            string commandName;
            string commandArgs;
            if (wordBoundary < 0)
            {
                commandName = query;
                commandArgs = String.Empty;
            }
            else
            {
                commandName = query.Substring(0, wordBoundary);
                commandArgs = query.Substring(wordBoundary).Trim();
            }
            if (InvocationTable.TryGetValue(commandName, out ICommand command))
            {
                return (command, commandArgs);
            }
            else
            {
                throw new ArgumentException($"Command not found: {commandName}");
            }
        }

        private static void Print(State state)
        {
            Console.SetCursorPosition(0, 2);
            foreach (var location in state.Locations)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(location.Key);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("] ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(location.Value.Path);
            }
        }
    }
}
