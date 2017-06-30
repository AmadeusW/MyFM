﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MyFm.Core.Commands
{
    public interface ICommand
    {
        string Name { get; }

        State Evaluate(State state, string args);
    }
}
