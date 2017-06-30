using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyFm.Core.Commands
{
    public class ExitCommand : ICommand
    {
        public string Name => "exit";

        public State Evaluate(State state, string args)
        {
            state.Running = false;
            return state;
        }
    }
}
