using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Cache;
using System.Net.Configuration;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.NetworkInformation;
using System.Net.Security;
using System.Net.Sockets;

using System.IO;

using System.Security.Cryptography;
using System.Reflection;

using System.Timers;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;
using System.Security.Principal;

namespace repouploader.hauptfunktionen
{
    class sontiges
    {
        public static void showHelp()
        {
           //Überarbeitung Mad (22 August 2014)
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Main Functions");
            Console.WriteLine("do() - Lädt eine Datei von einem gegebenen FTP Server herunter");
            Console.WriteLine("dos() - Lädt eine Datei von einem TLS / SSL gegebenen FTP Server herunter");
            Console.WriteLine("up() -  Lädt eine Datei auf einen FTP Server hoch");
            Console.WriteLine("sup() - Lädt eine Datei auf einen TLS / SSL gesicherten FTP Server hoch");
            Console.WriteLine("rm() - Löscht eine Datei auf dem FTP Server");
            Console.WriteLine("ping - Pingt den gegebenen FTP Server");
            Console.WriteLine(@"env --clear Löscht die Daten unter %APPDATA%\sharploader");
            Console.WriteLine("----------------------------------------------");

            Console.WriteLine("------------------------------------------");
            Console.WriteLine("                    ");
            Console.WriteLine("list() - Zeigt den aktuellen Ordner des eingeloggten Users an");
            Console.WriteLine("slist() - Gleiche wie list nur für einen SSL / TLS Server");
            Console.WriteLine("----------------------------------------------");

            Console.WriteLine("------------------------------------------");
            Console.WriteLine("                    ");
            Console.WriteLine("log - Zeigt die Log Dateien");
            Console.WriteLine("exit() - Schließt den  Sharploader");

        }

        public static void exit()
        {
            long memory = Environment.WorkingSet / 1024 / 1024;

            klassen.logger.__logger("\t1", "\tVerbrauchter RAM (nach exit und MB!): " + memory);


            klassen.logger.__logger("\t1", "Session ended at: " + DateTime.Now);
            klassen.logger.__logger("\t1", "EOF");
            Environment.Exit(0);
        }

        public static void clearconsole()
        {
            Console.Clear();
        }
    }
}
