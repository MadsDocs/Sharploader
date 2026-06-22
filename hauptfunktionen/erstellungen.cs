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
    class erstellungen
    {
        public static void makeftpdir()
        {
            try
            {
            //    Stopwatch watch08 = new Stopwatch();
                string user, password;
                string ftpserver = " ";
                string ordner;

                Console.WriteLine(" Benötige nun die Anmeldedaten!");
                Console.WriteLine(" User:");
                user = Console.ReadLine();


                Console.WriteLine(" Password: ");
                Console.ForegroundColor = ConsoleColor.Black;
                password = Console.ReadLine();
                Console.ResetColor();

                Console.WriteLine("FTP Server: ");
                ftpserver = Console.ReadLine();

                Console.WriteLine("Ordnername:");
                ordner = Console.ReadLine();

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri("ftp://" + ftpserver + "/" + ordner));

                string testftp = "ftp://" + ftpserver + "/" + ordner;
                Console.WriteLine(testftp);


                request.EnableSsl = true;
                request.Credentials = new NetworkCredential(user, password);
                //request.UsePassive = true;
                //request.KeepAlive = true;
                request.Method = WebRequestMethods.Ftp.MakeDirectory;

                WebResponse _sucess = request.GetResponse();
                Console.WriteLine("Debug: " + _sucess);

                //request.Method = WebRequestMethods.Ftp.MakeDirectory



            }
            catch (Exception ex)
            {
                klassen.logger._logger(1, ex.Message);
                klassen.logger._logger(1, ex.StackTrace);

                Console.WriteLine("Ein Fehler ist aufgetreten...");

            }
        }
    }
}
