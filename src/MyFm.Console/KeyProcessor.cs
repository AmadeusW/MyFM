using System;
using System.Collections.Generic;
using System.Text;

namespace MyFm.Console
{
    class KeyProcessor
    {

        public static void tempCode()
        {
            ArrayList al = new ArrayList();
            int tag = 0;

            ConsoleKeyInfo cki;

            StringBuilder sb = new StringBuilder();
            sb.Length = 0;

            do
            {
                cki = Console.ReadKey();

                if (cki.Key == ConsoleKey.Tab)
                {
                    for (int i = 0; i < saCommands.Length; i++)
                    {
                        if (fIsIn(sb.ToString(), saCommands[i].ToString()))
                            al.Add(saCommands[i].ToString());
                    }

                    if (al.Count != 0)
                    {
                        Console.Write(al[0].ToString());

                        for (int i = 0; i < al.Count; i++)
                        {
                            if (tag == 0)
                                Console.Clear();
                            if (tag == 0)
                                Console.Write(al[0].ToString());
                            ConsoleKeyInfo cki2;
                            cki2 = Console.ReadKey();

                            if (cki2.Key == ConsoleKey.Tab)
                            {
                                if ((tag + 1) < al.Count)
                                {
                                    Console.Clear();
                                    Console.Write(al[i + 1].ToString());
                                    tag++;
                                }
                                else
                                {
                                    tag = 0;
                                    i = -1;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    continue;
                }

                sb.Append(cki.Key.ToString());

            } while (cki.Key != ConsoleKey.Escape);

        }

    }
}
