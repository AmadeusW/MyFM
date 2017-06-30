using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyFm.Core.Commands
{
    public class SetPathCommand : ICommand
    {
        public string Name => "set";

        public State Evaluate(State state, string args)
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
    }
}
