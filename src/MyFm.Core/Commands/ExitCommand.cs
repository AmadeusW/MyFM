using MyFm.Core.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyFm.Core.Commands
{
    public class ExitCommand : ICommand, ILiveUpdate
    {
        string ICommand.Name => "exit";

        State ICommand.Evaluate(State state, string args)
        {
            state.Running = false;
            return state;
        }

        string ILiveUpdate.GetCompletion(State state, string query, int index)
        {
            return String.Empty;
        }

        string ILiveUpdate.ShowUpdate(State state, string query, int xOffset, int yOffset)
        {
            return "Press enter to exit";
        }
    }
}
