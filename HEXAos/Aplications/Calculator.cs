using System;

namespace HEXAos.Aplications
{
    class Calculator
    {
        public void Calc(string x)
        {
            if(x == "")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(Languages.Text("calc-welcome"));
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
            try
            {
                double n1;
                double n2;
                string opr;
                if (x == "")
                {
                    Console.Write(Languages.Text("calc-n1"));
                    n1 = double.Parse(SysConsole.ReadLine().Replace(",", "."));
                    Console.Write(Languages.Text("calc-n2"));
                    n2 = double.Parse(SysConsole.ReadLine().Replace(",", "."));

                    Console.Write(Languages.Text("calc-opr"));
                    opr = SysConsole.ReadLine().ToLower();
                }
                else
                {
                    string[] data = x.Split(" ");
                    n1 = double.Parse(data[0]);
                    n2 = double.Parse(data[2]);
                    opr = data[1];
                }

                string[] opr_list = new string[6] { "+", "-", "*", "=", "x", "==" };
                if (Array.IndexOf(opr_list, opr) > -1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(Languages.Text("calc-result"));
                }

                if (opr == "+")
                {
                    Console.WriteLine(n1 + n2);
                }
                else if (opr == "-")
                {
                    Console.WriteLine(n1 - n2);
                }
                else if (opr == "*" || opr == "x")
                {
                    Console.WriteLine(n1 * n2);
                }
                else if (opr == "/" || opr == ":")
                {
                    if (n2 != 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(Languages.Text("calc-result"));
                        Console.WriteLine(n1 / n2);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("calc: ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(Languages.Text("calc-divide-0"));
                    }
                }
                else if (opr == "=" || opr == "==")
                {
                    if (n1 == n2)
                    {
                        Console.WriteLine("True");
                    }
                    else
                    {
                        Console.WriteLine("False");
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("calc: ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(Languages.Text("calc-incorrect-opr"));
                }
            }
            catch (Exception except)
            {
                Console.ForegroundColor = ConsoleColor.White;
                if (except.Message == "ctrl+c")
                {
                    Console.WriteLine("^C");
                    return;
                }
                Console.Write("calc: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(Languages.Text("calc-incorrect-num"));
            }
        }
    }

}
