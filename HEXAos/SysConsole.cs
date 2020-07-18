using System;
using System.Collections.Generic;
using System.IO;
using Sys = Cosmos.System;

namespace HEXAos
{
    class SysConsole
    {

        public static List<string> cmdHistory = new List<string>();
        public static string ReadLine()
        {
            string temp = "";
            bool enter_pressed = false;

            while (!enter_pressed)
            {
                var cki = Console.ReadKey(true);
                if ((cki.Modifiers & ConsoleModifiers.Control) != 0)
                {
                    if (cki.Key == ConsoleKey.C) throw new Exception("ctrl+c");
                }
                else if (cki.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    enter_pressed = true;
                }
                else if (cki.Key == ConsoleKey.Backspace)
                {
                    if (temp.Length > 0)
                    {
                        temp = temp.Substring(0, temp.Length - 1);
                        int c_left = Console.CursorLeft;
                        int c_top = Console.CursorTop;
                        Console.SetCursorPosition(c_left - 1, c_top);
                        Console.Write(" ");
                        Console.SetCursorPosition(c_left - 1, c_top);
                    }
                }
                else
                {
                    Console.Write(cki.KeyChar);
                    temp += cki.KeyChar;
                }
            }

            return temp;
        }

        public static string ReadTermLine()
        {
            string temp = "";
            int hist_pos = -1;
            bool enter_pressed = false;

            while (!enter_pressed)
            {
                var cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    enter_pressed = true;
                    if (temp != "" || temp != cmdHistory[0])
                    {
                        cmdHistory.Insert(0, temp);
                    }
                }
                else if (cki.Key == ConsoleKey.Backspace)
                {
                    if (temp.Length > 0)
                    {
                        temp = temp.Substring(0, temp.Length - 1);
                        int c_left = Console.CursorLeft;
                        int c_top = Console.CursorTop;
                        Console.SetCursorPosition(c_left - 1, c_top);
                        Console.Write(" ");
                        Console.SetCursorPosition(c_left - 1, c_top);
                    }
                }
                else if (cki.Key == ConsoleKey.UpArrow)
                {
                    if (cmdHistory.Count > hist_pos + 1)
                    {
                        hist_pos += 1;
                        int chars = temp.Length;
                        int c_left = Console.CursorLeft;
                        int c_top = Console.CursorTop;
                        int c_temp = 0;
                        while (c_temp < chars)
                        {
                            Console.SetCursorPosition(c_left - 1, c_top);
                            Console.Write(" ");
                            Console.SetCursorPosition(c_left - 1, c_top);
                            c_left -= 1;
                            c_temp += 1;
                        }
                        temp = cmdHistory[hist_pos];
                        Console.Write(temp);
                    }
                }
                else if (cki.Key == ConsoleKey.DownArrow)
                {
                    if (hist_pos >= 0)
                    {
                        hist_pos -= 1;
                        int chars = temp.Length;
                        int c_left = Console.CursorLeft;
                        int c_top = Console.CursorTop;
                        int c_temp = 0;
                        while (c_temp < chars)
                        {
                            Console.SetCursorPosition(c_left - 1, c_top);
                            Console.Write(" ");
                            Console.SetCursorPosition(c_left - 1, c_top);
                            c_left -= 1;
                            c_temp += 1;
                        }
                        if (hist_pos != -1)
                        {
                            temp = cmdHistory[hist_pos];
                        }
                        Console.Write(temp);
                    }
                }
                else
                {
                    Console.Write(cki.KeyChar);
                    temp += cki.KeyChar;
                }
            }

            return temp;
        }

        public static void Terminal(bool apprunning, string appcommand)
        {
            try
            {
                Kernel.varibles[Kernel.varibles_names.IndexOf("path")] = Kernel.pathvar.TrimEnd('\\');
                string input;
                if (!apprunning)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("User@HEXAos");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(":");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    if (Kernel.pathvar == @"0:\")
                    {
                        Console.Write("~");
                    }
                    else
                    {
                        Console.Write("~" + Kernel.pathvar.Remove(0, 2));
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("# ");
                    input = ReadTermLine();
                }
                else
                {
                    input = appcommand;
                }
                input = input.Trim();
                foreach (string i in Kernel.varibles_names)
                {
                    if (input.EndsWith("$" + i) || input.Contains("$" + i + " ") || input.Contains("$" + i + '$') || input.Contains("$" + i + '\\'))
                    {
                        input = input.Replace("$" + i, Kernel.varibles[Kernel.varibles_names.IndexOf(i)]);
                    }
                }
                var command = input.ToLower();


                if (command == "shutdown" || command.StartsWith("shutdown "))
                {
                    Sys.Power.Shutdown();
                }
                else if (command == "reboot" || command.StartsWith("reboot "))
                {
                    Sys.Power.Reboot();
                }
                else if (command == "" || command == "echo")
                {
                    Console.Write("");
                }
                else if (command == "sysfiglet" || command.StartsWith("sysfiglet "))
                {
                    Kernel.SysFiglet();
                }
                else if (command == "help" || command.StartsWith("help "))
                {
                    Console.WriteLine(Kernel.Help());
                }
                else if (command == "clear" || command == "reset" || command.StartsWith("clear ") || command.StartsWith("reset "))
                {
                    Console.Clear();
                }
                else if (command == "ls" || command.StartsWith("ls ") || command == "dir" || command.StartsWith("dir "))
                {
                    string[] filePaths = Directory.GetFiles(Kernel.pathvar);
                    var drive = new DriveInfo("0");
                    Console.WriteLine(Languages.Text("dir-vol-name") + drive.VolumeLabel);
                    Console.WriteLine(Languages.Text("dir-of") + Kernel.pathvar);
                    Console.Write("\n");
                    for (int i = 0; i < filePaths.Length; i++)
                    {
                        string path = filePaths[i];
                        Console.WriteLine(Path.GetFileName(path));
                    }
                    foreach (var d in Directory.GetDirectories(Kernel.pathvar))
                    {
                        var dir = new DirectoryInfo(d);
                        var dirName = dir.Name;

                        Console.WriteLine(dirName + " <DIR>");
                    }
                    Console.Write("\n");
                    Console.WriteLine("        " + drive.TotalSize + Languages.Text("dir-bytes"));
                    Console.WriteLine("        " + drive.AvailableFreeSpace + Languages.Text("dir-bytes-free"));
                }
                else if (command == "pwd" || command.StartsWith("pwd ") || command == "cd")
                {
                    Console.WriteLine(Kernel.pathvar);
                }
                else if (command.StartsWith("cd "))
                {
                    string foldername = input.Remove(0, 3);
                    string[] dirs = Directory.GetDirectories(Kernel.pathvar);

                    List<string> dirslist = new List<string>();

                    foreach (string arrItem in dirs)
                    {
                        dirslist.Add(arrItem);
                    }

                    if (foldername == "..")
                    {
                        if (Kernel.pathvar == @"0:\")
                        {
                            Console.Write("cd: ");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(Languages.Text("cd-no-parent"));
                        }
                        else
                        {
                            string[] pathvarSplit = Kernel.pathvar.Split(@"\");
                            Kernel.pathvar = @"";
                            for (int l = 0; l < pathvarSplit.Length - 2; l++)
                            {
                                Kernel.pathvar = Kernel.pathvar + pathvarSplit[l] + @"\";
                            }
                        }
                    }
                    else if (foldername == @"0:\" || foldername == @"\" || foldername == "/" || foldername == "0:/")
                    {
                        Kernel.pathvar = @"0:\";
                    }
                    else
                    {
                        if (dirslist.Contains(foldername))
                        {
                            Kernel.pathvar = Kernel.pathvar + foldername + @"\";
                        }
                        else
                        {
                            Console.Write("cd: " + foldername + ": ");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(Languages.Text("cd-dir-not-found"));
                        }
                    }
                }
                else if (command.StartsWith("cat "))
                {

                    string filename = input.Remove(0, 4);

                    if (File.Exists(filename))
                    {
                        string f_contents = File.ReadAllText(filename);
                        Console.WriteLine(f_contents);
                    }
                    else if (File.Exists(Kernel.pathvar + filename))
                    {
                        string f_contents = File.ReadAllText(Kernel.pathvar + filename);
                        Console.WriteLine(f_contents);
                    }
                    else
                    {
                        Console.Write("cat: " + filename + ": ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(Languages.Text("cat-file-not-found"));
                    }
                }
                else if (command.StartsWith("echo "))
                {
                    string echotext = input.Remove(0, 5);
                    foreach (string i in Kernel.varibles_names)
                    {
                        if (echotext.EndsWith("$" + i) || echotext.Contains("$" + i + " ") || echotext.Contains("$" + i + "$"))
                        {
                            echotext = echotext.Replace("$" + i, Kernel.varibles[Kernel.varibles_names.IndexOf(i)]);
                        }
                    }
                    if (!apprunning) echotext += "\n";
                    Console.Write(echotext);
                }
                else if (command == "date" || command.StartsWith("date "))
                {
                    string Day = Cosmos.HAL.RTC.DayOfTheMonth.ToString();
                    string Mounth = Cosmos.HAL.RTC.Month.ToString();
                    string Year = Cosmos.HAL.RTC.Year.ToString();
                    string Hour = Cosmos.HAL.RTC.Hour.ToString();
                    string Minute = Cosmos.HAL.RTC.Minute.ToString();
                    string Cent = Cosmos.HAL.RTC.Century.ToString();
                    var Centaury = Cosmos.HAL.RTC.Century + 1;

                    Year = Cent + Year;

                    if (Day.Length < 2)
                    {
                        Day = "0" + Day;
                    }
                    if (Mounth.Length < 2)
                    {
                        Mounth = "0" + Mounth;
                    }
                    if (Hour.Length < 2)
                    {
                        Hour = "0" + Hour;
                    }
                    if (Minute.Length < 2)
                    {
                        Minute = "0" + Minute;
                    }

                    string temp = "th";
                    if (Centaury < 10 || Centaury > 20)
                    {
                        if (Centaury.ToString().EndsWith("1"))
                        {
                            temp = "st";
                        }
                        else if (Centaury.ToString().EndsWith("2"))
                        {
                            temp = "nd";
                        }
                        else if (Centaury.ToString().EndsWith("3"))
                        {
                            temp = "rd";
                        }
                    }
                    Console.WriteLine(Day + "." + Mounth + "." + Year + "  " + Hour + ":" + Minute + "  " + Centaury + temp + Languages.Text("date-centaury"));
                }
                else if (command.StartsWith("mkdir "))
                {
                    string dirname = input.Remove(0, 6);

                    if (Directory.Exists(Kernel.pathvar + dirname))
                    {
                        Console.Write("mkdir: " + dirname + ": ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(Languages.Text("mkdir-now-exists"));
                    }
                    else
                    {
                        try
                        {
                            Directory.CreateDirectory(Kernel.pathvar + dirname);
                        }
                        catch (Exception reason)
                        {
                            Console.Write("mkdir: " + dirname + ": ");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(reason.Message + "!\n");
                        }
                    }
                }
                else if (command.StartsWith("rmdir "))
                {
                    string dirToRemove = input.Remove(0, 6);

                    if (Directory.Exists(Kernel.pathvar + dirToRemove))
                    {
                        try
                        {
                            Directory.Delete(Kernel.pathvar + dirToRemove);
                        }
                        catch (Exception reason)
                        {
                            Console.Write("rmdir: " + dirToRemove + ": ");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(reason.Message + "!\n");
                        }
                    }
                    else
                    {
                        Console.Write("rmdir: " + dirToRemove + ": ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(Languages.Text("rmdir-not-found"));
                    }
                }
                else if (command.StartsWith("touch "))
                {
                    string filename = input.Remove(0, 6);

                    if (File.Exists(Kernel.pathvar + filename))
                    {
                        Console.Write("touch: " + filename + ": ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(Languages.Text("touch-now-exists"));
                    }
                    else
                    {
                        File.Create(Kernel.pathvar + filename);
                    }
                }
                else if (command.StartsWith("rm "))
                {
                    string filename = input.Remove(0, 3);

                    if (File.Exists(Kernel.pathvar + filename))
                    {
                        File.Delete(Kernel.pathvar + filename);
                    }
                    else
                    {
                        Console.Write("rm: " + filename + ": ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(Languages.Text("rm-not-exists"));
                    }
                }
                else if (command == "mkdir" || command == "rmdir" || command == "touch" || command == "rm" || command == "set")
                {
                    Console.Write(command + ": ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(Languages.Text("arg-required"));
                }
                else if (command == "sysinfo" || command.StartsWith("sysinfo "))
                {
                    Console.WriteLine(Languages.Text("sysinfo-os-name"));
                    Console.WriteLine(Languages.Text("sysinfo-os-ver") + Kernel.sysver);
                    Console.WriteLine(Languages.Text("sysinfo-total-ram") + (Cosmos.Core.CPU.GetAmountOfRAM() + 2) + "MB");
                }
                else if (command == "beep" || command.StartsWith("beep "))
                {
                    Console.Beep();
                }
                else if (command == "crashtest" || command.StartsWith("crashtest "))
                {
                    Kernel.ErrorScreen("Crash test");
                }
                else if (command == "ver" || command.StartsWith("ver "))
                {
                    Console.WriteLine(Languages.Text("ver") + Kernel.sysver);
                }
                else if (command == "calc" || command.StartsWith("calc "))
                {
                    Aplications.Calculator calc = new Aplications.Calculator();
                    if (command == "calc")
                    {
                        calc.Calc("");
                    }
                    else
                    {
                        calc.Calc(command.Remove(0, 5));
                    }

                }
                else if (command.StartsWith("set "))
                {
                    string[] pars = input.Remove(0, 4).Split("=");
                    if (pars.Length != 2)
                    {
                        Console.Write("set: ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(Languages.Text("set-incorrect-syntax"));
                        return;
                    }
                    else if (pars[0].Contains(" "))
                    {
                        Console.Write("set: ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(Languages.Text("set-var-spaces"));
                        return;
                    }
                    if (pars[1] == "input()")
                    {
                        pars[1] = ReadLine();
                    }

                    foreach (string i in Kernel.varibles_names)
                    {
                        if (pars[1].EndsWith("$" + i) || pars[1].Contains("$" + i + " ") || pars[1].Contains("$" + i + '$') || pars[1].Contains("$" + i + '\\'))
                        {
                            pars[1] = pars[1].Replace("$" + i, Kernel.varibles[Kernel.varibles_names.IndexOf(i)]);
                        }
                    }

                    if (Kernel.varibles_names.Contains(pars[0]))
                    {
                        Kernel.varibles[Kernel.varibles_names.IndexOf(pars[0])] = pars[1];
                    }
                    else
                    {
                        Kernel.varibles_names.Add(pars[0]);
                        Kernel.varibles.Add(pars[1]);
                    }
                }
                else if (command.StartsWith("run "))
                {
                    try
                    {
                        string filepath = input.Remove(0, 4);
                        if (File.Exists(filepath))
                        {
                            foreach (string fileline in File.ReadAllText(filepath).Split('\n'))
                            {
                                Terminal(true, fileline);
                            }
                        }
                        else if (File.Exists(Kernel.pathvar + filepath))
                        {
                            foreach (string fileline in File.ReadAllText(Kernel.pathvar + filepath).Split('\n'))
                            {
                                Terminal(true, fileline);
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("run: ");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(Languages.Text("run-not-exists"));
                        }
                    }
                    catch (Exception e)
                    {
                        if (e.Message == "ctrl+c")
                        {
                            return;
                        }
                    }
                }
                else
                {
                    string[] inputSplit = input.Split(" ");
                    Console.Write(inputSplit[0] + ": ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(Languages.Text("cmd-not-found"));
                }
            }
            catch (Exception reason)
            {
                Kernel.ErrorScreen(reason.Message);
            }
        }
    }
}
