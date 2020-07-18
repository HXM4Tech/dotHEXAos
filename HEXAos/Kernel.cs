using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.System.Network;
using System.IO;
using System.Threading;

namespace HEXAos
{
    public class Kernel : Sys.Kernel
    {
        public static string pathvar = @"0:\";
        public static string sysver = "BETA 1.36";
        public static List<string> varibles_names = new List<string>();
        public static List<string> varibles = new List<string>();
        public static string language_selected = "en_US";

        protected override void BeforeRun()
        {
            var fs = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            varibles_names.Add("path");
            varibles.Add(pathvar.TrimEnd('\\'));
            Langselect();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(Languages.Text("welcome"));
            Console.WriteLine(Languages.Text("help-info"));
            SysFiglet();
            Console.WriteLine("");
        }

        protected override void Run()
        {
            SysConsole.Terminal(false, string.Empty);
        }

        public static void Langselect()
        {
            Console.Clear();
            string window_ver = "                                                                 ";
            string window_hor = "  ";
            int posl = (Console.WindowWidth - window_ver.Length) / 2;
            Console.SetCursorPosition(posl, 3);
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.WriteLine(window_ver);
            int tmp = 4;
            Console.BackgroundColor = ConsoleColor.Blue;
            while (tmp != 15)
            {
                Console.SetCursorPosition(posl, tmp);
                Console.WriteLine(window_ver);
                tmp++;
            }
            Console.BackgroundColor = ConsoleColor.Magenta;
            tmp = 3;
            while(tmp != 16)
            {
                Console.SetCursorPosition(posl - 2, tmp);
                Console.WriteLine(window_hor);
                tmp++;
            }
            tmp = 3;
            while(tmp != 16)
            {
                Console.SetCursorPosition(posl + window_ver.Length, tmp);
                Console.WriteLine(window_hor);
                tmp++;
            }
            Console.SetCursorPosition(posl, tmp - 1);
            Console.WriteLine(window_ver);
            string title = "Select Language:";
            int pos_tit = (Console.WindowWidth - title.Length) / 2;
            Console.SetCursorPosition(pos_tit, 3);
            Console.WriteLine(title);
            Console.BackgroundColor = ConsoleColor.Blue;
            bool enter_pressed = false;
            int select = 0;
            Console.SetCursorPosition(posl + 2, 5);
            Console.WriteLine("[*] English (en_US)");
            Console.SetCursorPosition(posl + 2, 7);
            Console.WriteLine("[ ] Polski (pl_PL)");
            Console.SetCursorPosition(posl + 3, 12);
            Console.WriteLine("Navigate: [arrows - move] [enter - confirm]");
            Console.SetCursorPosition(posl + 3, 13);
            Console.WriteLine("Nawigacja: [strzalki - ruch] [enter - potwierdzenie]");
            while (!enter_pressed)
            {
                Console.SetCursorPosition(0, 0);
                var cki = Console.ReadKey(false);

                if(cki.Key == ConsoleKey.Enter)
                {
                    enter_pressed = true;
                }
                else if(cki.Key == ConsoleKey.DownArrow)
                {
                    select = 1;
                    Console.SetCursorPosition(posl + 2, 5);
                    Console.WriteLine("[ ]");
                    Console.SetCursorPosition(posl + 2, 7);
                    Console.WriteLine("[*]");
                }
                else if(cki.Key == ConsoleKey.UpArrow)
                {
                    select = 0;
                    Console.SetCursorPosition(posl + 2, 5);
                    Console.WriteLine("[*]");
                    Console.SetCursorPosition(posl + 2, 7);
                    Console.WriteLine("[ ]");
                }

            }

            if(select == 0)
            {
                language_selected = "en_US";
            }
            else
            {
                language_selected = "pl_PL";
            }

        }

        public static void SysFiglet()
        {
            var Figlet = @" _   _  _______   __  ___            
| | | ||  ___\ \ / / / _ \           
| |_| || |__  \ V / / /_\ \ ___  ___ 
|  _  ||  __| /   \ |  _  |/ _ \/ __|
| | | || |___/ /|\ \| | | | (_) \__ \
\_| |_/\____/\/   \/\_| |_/\___/|___/
                                     
                                     ";
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(Figlet);
        }

        public static string Help()
        {
            string helptext = Languages.Text("help-text");
            return helptext;
        }
        public static void ErrorScreen(string reason)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();


            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            
            string header = Languages.Text("RSOD-error");
            Console.SetCursorPosition((Console.WindowWidth - header.Length) / 2, Console.CursorTop);
            Console.WriteLine(header);

            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(Languages.Text("RSOD-error-long"));
            Console.WriteLine(Languages.Text("RSOD-exception") + reason);
            Console.WriteLine(Languages.Text("RSOD-version") + sysver);

            Console.WriteLine();

            Console.Write(Languages.Text("RSOD-press-reboot"));

            Console.ReadKey();

            Sys.Power.Reboot();
        }
    }
}
