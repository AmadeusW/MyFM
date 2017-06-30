using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyFm.Core.Commands
{
    public class ChangeDirectoryCommand : ICommand
    {
        public string Name => "cd";

        public State Evaluate(State state, string args)
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
    }
}
