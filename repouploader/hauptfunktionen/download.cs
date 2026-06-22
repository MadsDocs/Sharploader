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
        public static void downloader()
        {
            Stopwatch watch03 = new Stopwatch();
            watch03.Start();

            string user, password, file;
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

            try
            {


                FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + ftpserver + "/" + file);
                request.Method = WebRequestMethods.Ftp.DownloadFile;


                request.Credentials = new NetworkCredential(user, password);

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                StreamWriter _filetowrite = new StreamWriter(klassen.filesystem.appdata + @"\tmbsvn\" + file);
                _filetowrite.WriteLine(reader.ReadToEnd());

                _filetowrite.Flush();
                reader.Close();

                response.Close();
                Console.Clear();
                Console.WriteLine("Letzter Status: " + response.StatusDescription);

                watch03.Stop();
                klassen.logger.__logger("\t1\t", "Used Time by the DOWNLOAD Method: " + watch03.Elapsed);

            }
            catch (Exception ex)
            {

                klassen.logger._logger(1, ex.Message);
                klassen.logger._logger(1, ex.StackTrace);

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Letzter Fehler: {0} ", ex.Message);
                Console.ResetColor();
                Console.ReadLine();

                watch03.Stop();
                klassen.logger.__logger("\t1\t", "Used Time by the DOWNLOAD METHODE (SUB: EXCEPTION: " + ex.Message + ")" + watch03.Elapsed);
            }


        }

        public static void sDownload()
        {
            Stopwatch watch03 = new Stopwatch();
            watch03.Start();

            string user, password, file;
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

            try
            {


                FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + ftpserver + "/" + file);
                request.Method = WebRequestMethods.Ftp.DownloadFile;


                request.Credentials = new NetworkCredential(user, password);
                request.EnableSsl = true;

                klassen.logger.__logger("\t1\t", "IsSSL: true");

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                StreamWriter _filetowrite = new StreamWriter(klassen.filesystem.appdata + @"\tmbsvn\" + file);
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
                klassen.logger._logger(1, ex.Message);
                klassen.logger._logger(1, ex.StackTrace);

                watch03.Stop();
                klassen.logger.__logger("\t1\t", "Used Time by the DOWNLOAD METHODE (SUB: EXCEPTION: " + ex.Message + ")" + watch03.Elapsed);
            }
        }

        

    }
}
