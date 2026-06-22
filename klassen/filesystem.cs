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
using System.Windows.Forms;

using Microsoft.Win32;

namespace repouploader.klassen
{
    class filesystem
    {
        public static string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public static void _initdir()
        {
           
            try
            {
                //Dies hier wird benötigt damit der Sharploader Updater das Programm updaten kann :)
                RegistryKey sharp_reg = Registry.CurrentUser.CreateSubKey("sharploader");
                sharp_reg.SetValue("version", Application.ProductVersion);
                sharp_reg.SetValue("path", Environment.CurrentDirectory);



                if (!Directory.Exists(appdata + @"sharploader\"))
                {
                    Directory.CreateDirectory(appdata + @"\sharploader\");
                    klassen.logger._dummy("Dies ist eine Dummymessage, auch nur um zu sehen ob das SVN Dir richtig erstellt worden ist!");
                }

                if (Directory.Exists(appdata + @"\tmbsvn_backup"))
                {

                }

                //Hier werden wir eine Datei schreiben die den MD5 Wert (bestehend aus den Namen der Log Dateien) erstellen wird
                string svn = "sharploader.log";


                MD5 Hash = MD5.Create();
                byte[] data = Hash.ComputeHash(Encoding.UTF8.GetBytes(svn));
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sb.Append(data[i].ToString("x2"));
                }

                StreamWriter _md5 = new StreamWriter(filesystem.appdata + @"\sharploader\hash.txt");
                _md5.WriteLine(sb);
                _md5.Flush();
                _md5.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("INIT FAILED, please start the App as Admin!" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           

        }

        

        public static void openLogs()
        {
            if(!Directory.Exists(klassen.filesystem.appdata + @"\sharploader\"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(@"%APPDATA%\sharploader wurde nicht gefunden!");
                Console.ReadLine();
                Console.ResetColor();

                if (!EventLog.SourceExists("sharploader"))
                {
                    EventLog.CreateEventSource("sharploader","CannotOpenAppData");

                }

                EventLog sharploaderlog = new EventLog();
                sharploaderlog.Source = "sharploader";
                sharploaderlog.WriteEntry(@"Das Verzeichnis unter %APPDATA%\sharploader wurde nicht gefunden... Dies ist eine Sharploader Systemmessage");

                Directory.CreateDirectory(klassen.filesystem.appdata + @"\sharploader\");

            }
            else
            {
                string path = appdata + @"\sharploader\";
                Process.Start("explorer.exe", path);
            }


        }

        public static void writecmdifhttpfails()
        {

            if (File.Exists(appdata + @"\sharploader\cmd.txt"))
            {
                klassen.logger.__logger("\t1", "Backupcmd wurde schon geschrieben... aborting");
            }
            else
            {

                //Diese Mehtode wird aufgerufen falls der Download der Hilfedatei aus dem Internet fehlschlägt...
                StreamWriter _cmdcaller = new StreamWriter(appdata + @"\sharploader\cmd.txt", true, ASCIIEncoding.UTF8, 12);
                _cmdcaller.WriteLine("*******************************");
                _cmdcaller.WriteLine("Verfügbare Commands");
                _cmdcaller.WriteLine("up - Lädt eine Datei auf einen FTP Server hoch..");
                _cmdcaller.WriteLine("sup - Lädt eine Datei auf einen SSL geschützen Server hoch");
                _cmdcaller.WriteLine("do - Lädt eine Datei von einen Server runter.");
                _cmdcaller.WriteLine("dos - Lädt eine Datei von einem SSL geschützen Server runter");
                _cmdcaller.WriteLine("list - Listet den Ordner des aktuellen Benutzer auf");
                _cmdcaller.WriteLine("slist - Listet den Ordner des aktuellen Benutzer auf (SSL benötigt");
                _cmdcaller.WriteLine("version - Zeigt die aktuelle Sharploader Version an.");
                _cmdcaller.WriteLine("exit - Beendet das Programm");
                _cmdcaller.WriteLine("env - Zeigt das aktuelle Programmverzeichniss an.");
                _cmdcaller.WriteLine("env --clear Löscht die Log Dateien..");
                _cmdcaller.WriteLine("***************************************");
                _cmdcaller.Flush();
                _cmdcaller.Close();
            }
        }
    }
}
