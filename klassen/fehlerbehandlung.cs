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
using System.Windows.Forms;

namespace repouploader.klassen
{
    class fehlerbehandlung
    {

        public static void checkforallfiles()
        {

            try
            {
                Console.WriteLine("Willkommen beim Check Programm des Sharploaders!");
                Console.WriteLine("Dieses Programm überprüft ob alle benötigten Ordner vorhanden sind!");
                Console.WriteLine("------------------------------------------------------------------");

                if (!File.Exists(filesystem.appdata + @"\tmbsvn\hash.txt"))
                {
                    Console.WriteLine("Versuche nun den Ordner 'Sharploader' zu erstellen...");
                    filesystem._initdir();
                    Console.WriteLine("Der Ordner 'Sharploader' wurde erfolgreich erstellt!");

                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Keine Fehler, sollten Sie dennoch Fehler finden melden Sie diese bitte unter admin@themadbrainz.net");
                    Console.WriteLine("Oder melden Sie sich im Bugtracker unter Angabe des User: reporter und Passwort: reporter an");
                    Console.WriteLine("http://themadbrainz.net/bugtracker");
                    Console.WriteLine("                                    ");


                    Console.ReadLine();
                    klassen.logger.__logger("\t1", "                ");
                    klassen.logger.__logger("\t1", "\tÜberprüfung aller Daten erfolgreich beendet. Es wurden keine Fehlerhaften Daten oder fehlenden Daten gefunden!");
                    klassen.logger.__logger("\t1", "\tSollten dennoch Fehler gefunden worden sein, melden Sie sich bitte im Bugtracker als User reporter mit dem passwort reporter an!");
                    klassen.logger.__logger("\t1", " ");

                    Console.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler: ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
        public static void cleanup()
        {
            //Diese Methode benutz mal einfach den GC..
            GC.Collect();
            long totalmemory = GC.GetTotalMemory(true) / 1024 ;
            klassen.logger.__logger("\t3", "DEBUG: Anzahl der Generationen (GC): " + GC.MaxGeneration);
            klassen.logger.__logger("\t3", "DEBUG: TOTAL MEMORY: " + totalmemory);
        }


        public static void  GetMyExterniP()
        {
            WebRequest hwr = HttpWebRequest.Create(new Uri("http://checkip.dyndns.org"));
            WebResponse wr = hwr.GetResponse();
            Stream stream = wr.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);
            string htmlResult = streamReader.ReadToEnd();
            string[] htmlSplit = htmlResult.Split(new string[] { ":", "<" }, StringSplitOptions.RemoveEmptyEntries);
            string IP = htmlSplit[6].Trim();
            stream.Close();
            wr.Close();
            //return IPAddress.Parse(IP);

            Console.WriteLine("Externe IP: " + IPAddress.Parse(IP));
            Console.ReadLine();

        }

        public static void diagnose()
        {
            string antwort = " ";
            string lantwort = antwort.ToLower();


            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            Console.WriteLine("\t\r");

            GetMyExterniP();
            

            foreach (IPAddress adresse in host.AddressList)
            {
                Console.WriteLine("                          ");
                Console.WriteLine("IP: " + adresse.ToString());
                Console.WriteLine("SubNet: " + adresse.AddressFamily.ToString());

                klassen.logger.__logger("\t1", "IP: " + adresse.ToString());
                klassen.logger.__logger("\t1", "SubNet: " + adresse.AddressFamily.ToString());

                Console.WriteLine("                             ");

                //Console.ReadLine();
            }


            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Sollten Sie noch immer Probleme mit dem Hochladen der Datei haben");
            Console.WriteLine("Dann rufen Sie den Diagnosebefehl mit: ");
            Console.WriteLine("\tconfig --ping");
            Console.WriteLine("auf, damit können Sie sicherstellen das der FTP Server erreichbar ist!");
            Console.ResetColor();
            Console.ReadLine();
        }

       


    }
}
