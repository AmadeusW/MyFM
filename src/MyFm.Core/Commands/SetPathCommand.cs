using MyFm.Core.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyFm.Core.Commands
{
    public class SetPathCommand : ICommand, ILiveUpdate
    {
        string ICommand.Name => "set";

        State ICommand.Evaluate(State state, string args)
        {
            var wholeCommand = args.Trim();
            var wordBoundary = wholeCommand.IndexOf(' ');
            string moniker;
            string path;
            if (wordBoundary < 0)
            {
                moniker = wholeCommand;
                path = state.CurrentLocation.Path;
            }
            else
            {
                moniker = wholeCommand.Substring(0, wordBoundary);
                path = wholeCommand.Substring(wordBoundary).Trim();

                if (!Directory.Exists(path))
                {
                    throw new ArgumentException($"Not a valid directory: {args}");
                }
            }

            var location = new Location(path, false, false);
            state.AddLocation(location, moniker);

            return state;
        }

        string ILiveUpdate.GetCompletion(State state, string query, int index)
        {
            return String.Empty;
        }

        string ILiveUpdate.ShowUpdate(State state, string query, int xOffset, int yOffset)
        {
            string moniker = String.Empty;
            string path = String.Empty;
            query = query.Trim();
            var firstSpace = query.IndexOf(' ');
            if (firstSpace < 0)
            {
                moniker = query;
                path = state.CurrentLocation.Path;
            }
            else
            {
                moniker = query.Substring(0, firstSpace);
                path = Path.Combine(state.CurrentLocation.Path, query.Substring(firstSpace + 1));
            }

            return $"Assign [{moniker}] to {path}";
        }
    }
}
