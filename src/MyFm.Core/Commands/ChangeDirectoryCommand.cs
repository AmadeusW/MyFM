using MyFm.Core.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyFm.Core.Commands
{
    public class ChangeDirectoryCommand : ICommand, ILiveUpdate
    {
        string ICommand.Name => "cd";

        State ICommand.Evaluate(State state, string args)
        {
            if (Directory.Exists(args))
            {
                state.SetCurrentLocation(args);
                return state;
            }
            else
            {
                throw new ArgumentException($"Not a valid directory: {args}");
            }
        }

        string ILiveUpdate.GetCompletion(State state, string query, int index)
        {
            (string parentDirectory, string typedDirectory) = parseUserQuery(state, query);
            var suggestions = getSuggestions(parentDirectory, typedDirectory);
            return suggestions.Skip(index).FirstOrDefault();
        }

        string ILiveUpdate.ShowUpdate(State state, string query, int xOffset, int yOffset)
        {
            (string parentDirectory, string typedDirectory) = parseUserQuery(state, query);
            if (!Directory.Exists(parentDirectory))
            {
                return $"No such directory: {parentDirectory}";
            }
            var suggestions = getSuggestions(parentDirectory, typedDirectory);
            int i = 0;
            foreach (var suggestion in suggestions)
            {
                Console.SetCursorPosition(xOffset, yOffset + i++);
                Console.WriteLine(suggestion);
            }
            return parentDirectory;
        }

        private (string parentDirectory, string typedDirectory) parseUserQuery(State state, string query)
        {
            query = query.Trim();
            var lastSlash = query.LastIndexOf('/');
            string parentDirectory = state.CurrentLocation.Path;
            string typedDirectory = query;
            if (lastSlash >= 0)
            {
                parentDirectory += "/" + query.Substring(0, lastSlash);
                typedDirectory = query.Substring(lastSlash + 1);
            }
            return (parentDirectory, typedDirectory);
        }

        private IEnumerable<string> getSuggestions(string parentDirectory, string typedDirectory)
        {
            IEnumerable<string> suggestions;
            if (String.IsNullOrEmpty(typedDirectory))
            {
                suggestions = Directory.EnumerateDirectories(parentDirectory);
            }
            else
            {
                suggestions = Directory.EnumerateDirectories(parentDirectory, $"*{typedDirectory}*");
            }
            return suggestions;
        }
    }
}
