using System;
using MyFm.Core;
using MyFm.Core.Contracts;
using System.Linq;

namespace MyFm.Cli
{
    internal class CommandProvider : ILiveUpdate
    {
        private CommandProvider() { }
        private static CommandProvider _instance;
        public static CommandProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CommandProvider();
                };
                return _instance;
            }
        }

        string ILiveUpdate.GetCompletion(State state, string query, int index)
        {
            var matches = Program.InvocationTable.Keys.Where(n => n.Contains(query)).OrderBy(n => n);
            if (!matches.Any()) return String.Empty;
            index = index % matches.Count();
            return matches.Skip(index).FirstOrDefault();
        }

        string ILiveUpdate.ShowUpdate(State state, string query, int xOffset, int yOffset)
        {
            var matches = Program.InvocationTable.Keys.Where(n => n.Contains(query)).OrderBy(n => n);
            int i = 0;
            foreach (var match in matches)
            {
                Console.SetCursorPosition(xOffset, yOffset + i++);
                Console.Write(match);
            }
            return $"Commands matching {query}:";
        }
    }
}