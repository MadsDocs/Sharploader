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
            try
            {
                if (!File.Exists(klassen.filesystem.appdata + @"\sharploader\fail.txt"))
                {
                    WebClient client = new WebClient();
                    client.DownloadFile("https://files.themadbrainz.net:446/sharploader/commandtext/cmd.txt", klassen.filesystem.appdata + @"\tmbsvn\cmd.txt");
                    StreamReader _cmd = new StreamReader(klassen.filesystem.appdata + @"\tmbsvn\cmd.txt");
                    string inhalt = _cmd.ReadToEnd();

                    Console.WriteLine(inhalt);
                    Console.ReadLine();
                }
                else
                {

                    klassen.filesystem.writecmdifhttpfails();
                    StreamReader _callfailedcmd = new StreamReader(klassen.filesystem.appdata + @"\sharploader\cmd.txt");
                    string _cmd = _callfailedcmd.ReadToEnd();

                    Console.WriteLine(_cmd);
                }

            }
            catch (Exception ex)
            {
                File.Create(klassen.filesystem.appdata + @"\sharploader\fail.txt");

                klassen.logger._logger(1, ex.Message);
                klassen.logger._logger(1, ex.StackTrace);

                klassen.logger.__logger("\t1", "Konnte die Hilfedatei nicht anlegen, werde nun eine lokale Kopie erstellen..");
                klassen.filesystem.writecmdifhttpfails();

                StreamReader _callfailedcmd = new StreamReader(klassen.filesystem.appdata + @"\sharploader\cmd.txt");
                string _cmd = _callfailedcmd.ReadToEnd();

                Console.WriteLine(_cmd);
            }

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
