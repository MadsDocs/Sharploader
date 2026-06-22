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

namespace repouploader
{
    class uploader
    {
        public static void init()
        {
            Stopwatch watch06 = new Stopwatch();
            try
            {
                //Dies ist jetzt der eigentl. Init Block (hier standen vorher sachen die locker in ein paar methoden passen konnten, siehe auch filesystem.cs
                klassen.filesystem._initdir(); //Dies ist SEEEEEEHHHRRR wichtig da sonst das ganze Konstrukt zusammenfällt!
                
                Stopwatch watch01 = new Stopwatch();
                watch01.Start();

                string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                Console.Title = "FTP Uploader by The Mad Brainz | Version: " + version + " (Deus Ex Machina) x64 Version";

                string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                //Paar OS Informationen:
                string osversion = Environment.OSVersion.VersionString;
                string user = Environment.UserName;
                string duser = Environment.UserDomainName;
                string clr_ver = Environment.Version.ToString();
                string svnver = Assembly.GetExecutingAssembly().GetName().Version.ToString();

                foreach (var screen in Screen.AllScreens)
                {
                    klassen.logger.__logger("\t1", " -- Device Specifications -- ");
                    //klassen.logger.__logger("1", "Bounds: " + screen.Bounds.toString());
                    klassen.logger.__logger("\t1", "Device Name: " + screen.DeviceName);
                    klassen.logger.__logger("\t1", "Primary: " + screen.Primary.ToString());
                    //klassen.logger.__logger("1", "Working Area: " + screen.WorkingArea);
                }


                bool isAdmin = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

                klassen.logger.__logger("\t1", "--- Programmstart at [" + DateTime.Now + "]");
                klassen.logger.__logger("\t1", "OS: " + osversion);
                klassen.logger.__logger("\t1", "User: " + user);
                klassen.logger.__logger("\t1", "Domain: " + duser);
                klassen.logger.__logger("\t1", "CLR VER: " + clr_ver);
                klassen.logger.__logger("\t1", "SVN Version: " + svnver);
                klassen.logger.__logger("\t1", "IsAdmin: " + isAdmin);
                klassen.logger.__logger("\t", "                        ");


                Console.WriteLine(" ");
                Console.WriteLine("SVN Uploader");
                Console.WriteLine("Geben Sie help ein, für die verfügbaren Befehle..");
                Console.WriteLine("Oder drücken Sie einfach die Enter Taste!");
                Console.WriteLine("Handbuch gibt ihnen einen kleinen Überblick!");
                Console.WriteLine("Für etwaige Fehlermeldungen lesen Sie unter: ");
                Console.WriteLine("https://files.themadbrainz.net:446/sharploader/Support");

                do
                {
                    Console.WriteLine("Drücken Sie ENTER für eine Übersicht aller Commands...");
                    Console.WindowHeight = 60;
                    Console.WindowWidth = 85;

                    string befehl = Console.ReadLine();

                    string neubefehl = befehl.ToLower();

                    switch (neubefehl)
                    {
                        case "up()":
                            hauptfunktionen.upload.uploader();
                            break;
                        case "tup()":
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("NOT IMPLEMENTED");
                            Console.ResetColor();
                            break;
                        case "sup()":
                            hauptfunktionen.upload.suploader();
                            break;
                        case "do()":
                            hauptfunktionen.download.downloader();
                            break;
                        case "dos()":
                            hauptfunktionen.download.sDownload();
                            break;
                        case "list()":
                            hauptfunktionen.lister.showDir();
                            break;
                        case "slist()":
                            hauptfunktionen.lister.sList();
                            break;
                        case "mkdir()":
                            Console.WriteLine("Geben Sie einen Ordnernamen ein: ");
                            string ordnername = Console.ReadLine();
                            hauptfunktionen.erstellungen.makeftpdir(ordnername);
                            break;
                        case "rm()":
                            //Dies ist der löschen Befehl für den Sharploader...
                            dev.devfun.deletegivefileonftp();
                            break;
                        case "exit()":
                            hauptfunktionen.sontiges.exit();
                            break;
                        case "auto --createfile":
                            dev.devfun.createfileforautologin();
                            break;
                        case "version":
                            dev.devfun.showversion();
                            break;
                        case "version --check":
                            //SystemMethoden.updater.doUpdate();
                            break;
                        case "env":
                            dev.devfun.showenv();
                            break;
                        case "log":
                            klassen.filesystem.openLogs();
                            break;
                        case "cls":
                            hauptfunktionen.sontiges.clearconsole();
                            break;
                        case "env --clear":
                            dev.devfun.clearenv();
                            break;
                        case "ping":
                            string ftpserver = " ";
                            Console.WriteLine("Bitte geben Sie einen FTP Server zum suchen ein..");
                            ftpserver = Console.ReadLine();
                            dev.devfun.searchforgivenftpserver(ftpserver);
                            break;
                        case "handbuch":
                            klassen.fehlerbehandlung.Handbuch();
                            break;
                        case "cleanup":
                            klassen.fehlerbehandlung.cleanup();
                            break;
                        case "config":
                            klassen.fehlerbehandlung.diagnose();
                            break;
                        case "config --ping":
                            Console.WriteLine("Geben Sie bitte einen FTP Server an: ");
                            ftpserver = Console.ReadLine();
                            dev.devfun.searchforgivenftpserver(ftpserver);
                            break;
                        case "dev.split":
                            dev.devfun.splitfile(100000, @"P:\TestFile.txt");
                            break;
                        default:
                            klassen.logger.__logger("\t1", "Nicht vorhandenes Kommando: " + befehl);
                            hauptfunktionen.sontiges.showHelp();
                            break;
                    }

                    watch01.Stop();
                    klassen.logger.__logger("\t1", "Used Time by the CMD Method:  " + watch01.Elapsed);

                } while (true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
           
        }
    }

}
