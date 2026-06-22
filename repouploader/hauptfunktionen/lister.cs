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
using System.Data.SqlClient;
using System.Windows.Forms;

using System.Threading;

using System.Security;
using System.Security.AccessControl;
using System.Security.Authentication;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security.Principal;
using System.Security.Util;

namespace repouploader.hauptfunktionen
{
    class lister
    {
        public static void showDir()
        {
            Stopwatch watch04 = new Stopwatch();
            watch04.Start();

            string user, password;
            string ftpserver = " ";

            Console.WriteLine(" Benötige nun die Anmeldedaten!");
            Console.WriteLine(" User:");
            user = Console.ReadLine();


            Console.WriteLine(" Password: ");
            Console.ForegroundColor = ConsoleColor.Black;
            password = Console.ReadLine();
            Console.ResetColor();

            Console.WriteLine("FTP Server: ");
            ftpserver = Console.ReadLine();


            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + ftpserver);
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;


            try
            {
                if (ftpserver.StartsWith("ftp://"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("LIST abgebrochen, bitte den Servernamen OHNE ftp angeben!");
                    Console.ResetColor();

                    klassen.logger.__logger("\t1", "LIST Method aborted, ftp:// detected!");
                }
                else
                {

                    request.Credentials = new NetworkCredential(user, password);
                    FtpWebResponse ftprespone = (FtpWebResponse)request.GetResponse();

                    Stream stream_respone = ftprespone.GetResponseStream();
                    StreamReader reader = new StreamReader(stream_respone);

                    Console.WriteLine(reader.ReadToEnd());

                    Console.ReadLine();
                }

            }
            catch (Exception ex)
            {
                klassen.logger._logger(3, ex.Message);
                watch04.Stop();
                klassen.logger.__logger("\t1\t", "Used Time by the SHOWDIR Method:(SUB: EXCEPTION: " + ex.Message + watch04.Elapsed);
            }

            watch04.Stop();
            klassen.logger.__logger("\t1\t", "Used Time by the SHOWDIR Method: " + watch04.Elapsed);

        }

        public static void sList()
        {
            Stopwatch watch04 = new Stopwatch();
            watch04.Start();

            string user, password;
            string ftpserver = " ";

            Console.WriteLine(" Benötige nun die Anmeldedaten!");
            Console.WriteLine(" User:");
            user = Console.ReadLine();


            Console.WriteLine(" Password: ");
            Console.ForegroundColor = ConsoleColor.Black;
            password = Console.ReadLine();
            Console.ResetColor();

            Console.WriteLine("FTP Server: ");
            ftpserver = Console.ReadLine();


            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + ftpserver);
            request.EnableSsl = true;
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;


            try
            {
                if (ftpserver.StartsWith("ftp://"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("LIST abgebrochen, bitte den Servernamen OHNE ftp angeben!");
                    Console.ResetColor();

                    klassen.logger.__logger("\t1", "LIST Method aborted, ftp:// detected!");
                }
                else
                {

                    request.Credentials = new NetworkCredential(user, password);
                    FtpWebResponse ftprespone = (FtpWebResponse)request.GetResponse();

                    Stream stream_respone = ftprespone.GetResponseStream();
                    StreamReader reader = new StreamReader(stream_respone);

                    Console.WriteLine(reader.ReadToEnd());

                    Console.ReadLine();
                }

            }
            catch (Exception ex)
            {
                klassen.logger._logger(3, ex.Message);
                watch04.Stop();
                klassen.logger.__logger("\t1\t", "Used Time by the SHOWDIR Method:(SUB: EXCEPTION: " + ex.Message + watch04.Elapsed);
            }

            watch04.Stop();
            klassen.logger.__logger("\t1\t", "Used Time by the SHOWDIR Method: " + watch04.Elapsed);

        }
    }
}
