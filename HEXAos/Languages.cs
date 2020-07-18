using System;
using System.Collections.Generic;
using System.Text;

namespace HEXAos
{
    class Languages
    {
        public static string Text(string toTranslate)
        {
            switch(Kernel.language_selected)
            {
                case "en_US":
                    switch(toTranslate)
                    {
                        case "welcome":
                            return "Welcome to HEXAos!";
                        case "help-info":
                            return "Type \"help\" to get list of commands.";
                        case "help-text":
                            string helptext = @"List of commands:
help - this list
sysfiglet - display system figlet
shutdown - power off comuter
reboot - restart computer
clear | reset - clear screen
dir | ls - display files and folders in this location
pwd - print working directory
cd - change directory
cat - display file content
echo - write text
date - print date and time
mkdir - make directory
rmdir - remove directory
touch - create file
rm - remove file
sysinfo - display system info
crashtest - test error screen
calc - calculator
";
                            return helptext;
                        case "RSOD-error":
                            return "  HEXAos Error!  ";
                        case "RSOD-error-long":
                            return "An error occured in HEXAos!";
                        case "RSOD-exception":
                            return "Exception: ";
                        case "RSOD-version":
                            return "HEXAos version: ";
                        case "RSOD-press-reboot":
                            return "Press any key to reboot your PC...";
                        case "dir-vol-name":
                            return "Volume in drive 0 is ";
                        case "dir-of":
                            return "Directory of ";
                        case "dir-bytes":
                            return " bytes";
                        case "dir-bytes-free":
                            return " bytes free";
                        case "cd-no-parent":
                            return "This directory has no parent!\n";
                        case "cd-dir-not-found":
                            return "Directory not found!\n";
                        case "cat-file-not-found":
                            return "File not found!\n";
                        case "date-centaury":
                            return "Centaury";
                        case "mkdir-now-exists":
                            return "This directory now exits!\n";
                        case "rmdir-not-found":
                            return "Directory not found!\n";
                        case "touch-now-exists":
                            return "This file is now exist!\n";
                        case "rm-not-exists":
                            return "This file is not exist!\n";
                        case "arg-required":
                            return "This command requires an argument!\n";
                        case "sysinfo-os-name":
                            return "OS Name:      HEXAos";
                        case "sysinfo-os-ver":
                            return "OS Version:   ";
                        case "sysinfo-total-ram":
                            return "Total RAM:    ";
                        case "ver":
                            return "HEXAos version ";
                        case "set-incorrect-syntax":
                            return "Incorrect syntax!";
                        case "set-var-spaces":
                            return "Varible name can't contains spaces!";
                        case "run-not-exists":
                            return "Script not exist!";
                        case "cmd-not-found":
                            return "Command not found!\n";
                        case "calc-welcome":
                            return "Welcome to EasyCalc!";
                        case "calc-n1":
                            return "First number: ";
                        case "calc-n2":
                            return "Second number: ";
                        case "calc-opr":
                            return "Operation: ";
                        case "calc-result":
                            return "Your result is: ";
                        case "calc-divide-0":
                            return "Do not divide by 0!";
                        case "calc-incorrect-opr":
                            return "Incorrect operation\n";
                        case "calc-incorrect-num":
                            return "Incorrect number input\n";
                    }
                    break;
                case "pl_PL":
                    switch (toTranslate)
                    {
                        case "welcome":
                            return "Witaj w HEXAos!";
                        case "help-info":
                            return "Wpisz \"help\" aby otrzymac liste komend.";
                        case "help-text":
                            string helptext = @"Lista komend:
help - ta lista
sysfiglet - wyswietla grafike systemu
shutdown - wylacza komputer
reboot - restartuje komputer
clear | reset - czysci ekran
dir | ls - wyswietla pliki i katalogi w obecnej lokalizacji
pwd - wyswietla uzywany katalog
cd - zmienia uzywany katalog
cat - wyswietla zawartosc pliku
echo - zwraca wpisany tekst
date - wyswietla date i godzine
mkdir - tworzy katalog
rmdir - usuwa katalog
touch - tworzy plik
rm - usuwa plik
sysinfo - wyswietla informacje o systemie
crashtest - testuje ekran bledu
calc - kalkulator
";
                            return helptext;
                        case "RSOD-error":
                            return "  Blad HEXAos!  ";
                        case "RSOD-error-long":
                            return "Wystapil blad w systemie!";
                        case "RSOD-exception":
                            return "Informacje: ";
                        case "RSOD-version":
                            return "wersja HEXAos: ";
                        case "RSOD-press-reboot":
                            return "Nacisnij dowolny klawisz, aby zrestartowac komputer...";
                        case "dir-vol-name":
                            return "Nazwa dysku 0 to ";
                        case "dir-of":
                            return "Pliki i katalogi w ";
                        case "dir-bytes":
                            return " bajtow";
                        case "dir-bytes-free":
                            return " bajtow wolnych";
                        case "cd-no-parent":
                            return "Brak katalogu nadrzednego!\n";
                        case "cd-dir-not-found":
                            return "Nie znaleziono katalogu!\n";
                        case "cat-file-not-found":
                            return "Plik nie znaleziony!\n";
                        case "date-centaury":
                            return "Wiek";
                        case "mkdir-now-exists":
                            return "Ten katalog juz istnieje!\n";
                        case "rmdir-not-found":
                            return "Nie znaleziono katalogu!\n";
                        case "touch-now-exists":
                            return "Ten plik juz istnieje!\n";
                        case "rm-not-exists":
                            return "Nie znaleziono pliku!\n";
                        case "arg-required":
                            return "Ta komenda wymaga argumentu!\n";
                        case "sysinfo-os-name":
                            return "Nazwa sys.:   HEXAos";
                        case "sysinfo-os-ver":
                            return "Wersja sys:   ";
                        case "sysinfo-total-ram":
                            return "Pamiec RAM:   ";
                        case "ver":
                            return "HEXAos wersja ";
                        case "set-incorrect-syntax":
                            return "Nieprawidlowa skladnia!";
                        case "set-var-spaces":
                            return "Nazwy zmiennych nie moga zawierac spacji!";
                        case "run-not-exists":
                            return "Skrypt nie istnieje!";
                        case "cmd-not-found":
                            return "Nie znaleziono polecenia!\n";
                        case "calc-welcome":
                            return "Witamy w EasyCalc!";
                        case "calc-n1":
                            return "Pierwsza liczba: ";
                        case "calc-n2":
                            return "Druga liczba: ";
                        case "calc-opr":
                            return "Operacja: ";
                        case "calc-result":
                            return "Wynik to: ";
                        case "calc-divide-0":
                            return "Nie wolno dzielic przez 0!";
                        case "calc-incorrect-opr":
                            return "Nieprawidlowa operacja\n";
                        case "calc-incorrect-num":
                            return "Nieprawidlowa liczba\n";
                    }
                    break;
            }
            return "";
        }
    }
}
