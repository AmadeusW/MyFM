using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using MyFm.Core.Contracts;
using MyFm.Core;

namespace MyFm.Cli
{
    class KeyProcessor
    {
        StringBuilder _query;

        public KeyProcessor()
        {
            _query = new StringBuilder();
        }
        int LastChar => _query.Length - 1;
        int InsertionPoint = 0;

        internal string ReadCommand(State state)
        {
            ILiveUpdate provider = null;
            int tabCount = 0;
            while (true)
            {
                Console.SetCursorPosition(InsertionPoint, 1);
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.Backspace:
                        _query.Remove(LastChar, 1);
                        InsertionPoint--;
                        break;

                    case ConsoleKey.Delete:
                        _query.Remove(InsertionPoint, 1);
                        break;

                    case ConsoleKey.RightArrow:
                        if (InsertionPoint < _query.Length)
                            InsertionPoint++;
                        break;

                    case ConsoleKey.LeftArrow:
                        if (InsertionPoint > 0)
                            InsertionPoint--;
                        break;

                    case ConsoleKey.Tab:

                        var firstSpace_ = _query.ToString().IndexOf(' ');
                        string completedString_ = String.Empty;
                        if (firstSpace_ >= 0)
                        {
                            var providerName_ = _query.ToString().Substring(0, firstSpace_);
                            if (Program.InvocationTable.TryGetValue(providerName_, out ICommand command_))
                            {
                                var provider_ = command_ as ILiveUpdate;
                                if (provider != null)
                                {
                                    var args = _query.ToString().Substring(firstSpace_ + 1);
                                    completedString_ = provider.GetCompletion(state, args, tabCount);
                                }
                            }
                        }
                        else
                        {
                            // Ask the provider
                            completedString_ = (CommandProvider.Instance as ILiveUpdate).GetCompletion(state, _query.ToString(), tabCount);
                        }

                        Console.SetCursorPosition(0, 1);
                        Console.Write(completedString_);
                        InsertionPoint = completedString_.Length;
                        break;

                    case ConsoleKey.Escape:
                        return String.Empty;

                    case ConsoleKey.Enter:
                        return _query.ToString();

                    default:
                        _query.Append(key.KeyChar);
                        InsertionPoint++;
                        break;
                }
                tabCount = key.Key == ConsoleKey.Tab ? tabCount + 1 : 0;
                for (int line = 9; line < 20; line++) // Clear the update area
                {
                    Console.SetCursorPosition(0, line);
                    Console.Write("                                                    ");
                }

                var firstSpace = _query.ToString().IndexOf(' ');
                var providerName = firstSpace < 0 ? _query.ToString() : _query.ToString().Substring(0, firstSpace);

                string tip = String.Empty;
                Console.ForegroundColor = ConsoleColor.White;
                if (Program.InvocationTable.TryGetValue(providerName, out ICommand command))
                {
                    var provider_ = command as ILiveUpdate;
                    if (provider != null)
                    {
                        var args = _query.ToString().Substring(firstSpace + 1);
                        tip = provider?.ShowUpdate(state, _query.ToString(), 0, 10);
                    }
                }
                else
                {
                    tip = (CommandProvider.Instance as ILiveUpdate).ShowUpdate(state, _query.ToString(), 0, 10);
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(0, 9);
                Console.Write(tip);
            }
        }
    }
}
