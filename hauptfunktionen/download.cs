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


    class download
    {
        private static string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public static void downloader()
        {
             Stopwatch watch03 = new Stopwatch();
            watch03.Start();

            string user, password, file, lfile, newhdd;
            string hdd = " ";
            string ftpserver = "";
            Console.WriteLine(" Benötige nun die Anmeldedaten und die runter zu ladene Datei!");
            Console.WriteLine(" User:");
            user = Console.ReadLine();


            Console.WriteLine(" Password: ");
            Console.ForegroundColor = ConsoleColor.Black;
            password = Console.ReadLine();
            Console.ResetColor();

            Console.WriteLine("FTP Server");
            ftpserver = Console.ReadLine();


            Console.WriteLine(" Datei: ");
            file = Console.ReadLine();

            Console.WriteLine("Lokale Datei:");
            lfile = Console.ReadLine();

            try
            {

                string frequest = "ftp://" + ftpserver + "/" +  file;


                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(frequest);
                request.Method = WebRequestMethods.Ftp.DownloadFile;


                request.Credentials = new NetworkCredential(user, password);
                request.EnableSsl = true;

                klassen.logger.__logger("\t1\t", "IsSSL: true");

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Console.WriteLine("Trying to Download the File: {0}", frequest);

                DriveInfo[] verFestplatte = DriveInfo.GetDrives();
                //newhdd = hdd.Substring(0, 3);

                foreach (DriveInfo d  in verFestplatte)
                {
                       if(d.IsReady)
                       {
                           klassen.logger.__logger("1", d + " is reporting for Duty!");
                           klassen.logger.__logger("1", d.VolumeLabel);
                       }
                }


                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                StreamWriter _filetowrite = new StreamWriter(lfile);
                _filetowrite.WriteLine(reader.ReadToEnd());

                _filetowrite.Flush();
                reader.Close();

                response.Close();
                Console.Clear();
                Console.WriteLine("Letzter Status: " + response.StatusDescription);

                watch03.Stop();
                klassen.logger.__logger("\t1\t", "Used Time by the DOWNLOAD Method: " + watch03.Elapsed);
                klassen.logger.__logger("\t1\t", "Letzter Status: " + response.StatusDescription);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Sorry, while Processing your Downloadrequest a Error occured!");
                Console.WriteLine(ex.Message);

                Console.ReadLine();

                klassen.logger._logger(1, ex.Message);
                klassen.logger._logger(1, ex.StackTrace);

                watch03.Stop();
                klassen.logger.__logger("\t1\t", "Used Time by the DOWNLOAD METHODE (SUB: EXCEPTION: " + ex.Message + ")" + watch03.Elapsed);
            }
        }

        public static void sDownload()
        {
            Stopwatch watch03 = new Stopwatch();
            watch03.Start();

            string user, password, file, lfile, newhdd;
            string hdd = " ";
            string ftpserver = "";
            Console.WriteLine(" Benötige nun die Anmeldedaten und die runter zu ladene Datei!");
            Console.WriteLine(" User:");
            user = Console.ReadLine();


            Console.WriteLine(" Password: ");
            Console.ForegroundColor = ConsoleColor.Black;
            password = Console.ReadLine();
            Console.ResetColor();

            Console.WriteLine("FTP Server");
            ftpserver = Console.ReadLine();


            Console.WriteLine(" Datei: ");
            file = Console.ReadLine();

            Console.WriteLine("Lokale Datei:");
            lfile = Console.ReadLine();

            try
            {

                string frequest = "ftp://" + ftpserver + "/" +  file;


                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(frequest);
                request.Method = WebRequestMethods.Ftp.DownloadFile;


                request.Credentials = new NetworkCredential(user, password);
                request.EnableSsl = true;

                klassen.logger.__logger("\t1\t", "IsSSL: true");

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Console.WriteLine("Trying to Download the File: {0}", frequest);

                DriveInfo[] verFestplatte = DriveInfo.GetDrives();
                //newhdd = hdd.Substring(0, 3);

                foreach (DriveInfo d  in verFestplatte)
                {
                       if(d.IsReady)
                       {
                           klassen.logger.__logger("1", d + " is reporting for Duty!");
                           klassen.logger.__logger("1", d.VolumeLabel);
                       }
                }


                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                StreamWriter _filetowrite = new StreamWriter(lfile);
                _filetowrite.WriteLine(reader.ReadToEnd());

                _filetowrite.Flush();
                reader.Close();

                response.Close();
                Console.Clear();
                Console.WriteLine("Letzter Status: " + response.StatusDescription);

                watch03.Stop();
                klassen.logger.__logger("\t1\t", "Used Time by the DOWNLOAD Method: " + watch03.Elapsed);
                klassen.logger.__logger("\t1\t", "Letzter Status: " + response.StatusDescription);


                FileInfo _file = new FileInfo(lfile);
                string ex = _file.Extension;

                if (ex == ".exe")
                {
                    klassen.logger.__logger("1", ".exe Fileextension detected... Navigation aborted...");
                    Console.WriteLine("Sharploader hat eine exe Datei entdeckt, Bitte aktivieren Sie in den Einstellungen die Option 'exe Dateien öffnen' wenn Sie wollen das Sharploder");
                    Console.WriteLine("exe Datei automatisch ausführt!");

                    Console.ReadLine();
                }
                else
                {
                    Process.Start("explorer.exe", lfile);
                }
                
                
                


            }
            catch (Exception ex)
            {
                Console.WriteLine("Sorry, while Processing your Downloadrequest a Error occured!");
                Console.WriteLine(ex.Message);


            

                Console.ReadLine();

                klassen.logger._logger(1, ex.Message);
                klassen.logger._logger(1, ex.StackTrace);

                watch03.Stop();
                klassen.logger.__logger("\t1\t", "Used Time by the DOWNLOAD METHODE (SUB: EXCEPTION: " + ex.Message + ")" + watch03.Elapsed);
            }
        }

        

    }
}
