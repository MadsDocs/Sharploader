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

namespace repouploader.dev
{
    class devfun
    {
        public static void updev()
        {
            string _selfloc = Environment.CurrentDirectory;
            Console.WriteLine("DEBUG: " + _selfloc);
            klassen.logger.__logger("1", _selfloc);


            devupload("uploader", "uploader", "sharploader.exe");

        }


        /// <summary>
        /// Mit dieser Methode können die Entwickler ihre neuerste Version auf den TheMadBrainz Server hochladen!
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="file"></param>
        //[System.Obsolete("Use the updev Method instead!")]
        public static void devupload(string user, string password, string file)
        {
            FileInfo _uploadfile = new FileInfo(file);
            string name = _uploadfile.Name;
            string ext = _uploadfile.Extension;
            long groesse = _uploadfile.Length;

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://themadbrainz.net/" + name);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(user, password);
            Console.WriteLine("Versuche nun die Datei {0} hoch zu laden...", name);
            StreamReader _sourcestream = new StreamReader(file);
            byte[] filecontens = Encoding.UTF8.GetBytes(_sourcestream.ReadToEnd());
            _sourcestream.Close();
            request.ContentLength = file.Length;

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(filecontens, 0, filecontens.Length);
            requestStream.Close();

            FtpWebResponse ftprespone = (FtpWebResponse)request.GetResponse();
            Console.Clear();

            klassen.logger.__logger("3", "Upload der Datei " + name + ext + "mit der Grösse: " + groesse + " wurde erfolgreich hochgeladen!");
            Console.WriteLine("Letzter Status: {0}", ftprespone.StatusDescription);
        }

        /// <summary>
        /// Methode um herauszufinden was das aktuelle WorkDir ist, nützlich fürs Updaten..
        /// </summary>
        public static void showenv()
        {
            string curr_dir = Environment.CurrentDirectory;
            Console.WriteLine("Derzeitiges WorkingDir: {0}", curr_dir);
            klassen.logger.__logger("\t1", "Current Dir: " + curr_dir);

            StreamWriter _currdir = new StreamWriter(klassen.filesystem.appdata + @"\tmbsvn\currdir.txt");
            _currdir.WriteLine(curr_dir);
            _currdir.Flush();
            _currdir.Close();
        }

        public static void clearenv()
        {
            try
            {
                Console.WriteLine("Wollen Sie die Daten vorher sichern?");
                string antwort = Console.ReadLine();

                string neuantwort = antwort.ToLower();

                switch (neuantwort)
                {
                    case "j":

                        if (!Directory.Exists(klassen.filesystem.appdata + @"\sharploader_backup\"))
                        {
                            Directory.CreateDirectory(klassen.filesystem.appdata + @"\sharploader_backup\");

                            //svn.log:
                            if (!File.Exists(klassen.filesystem.appdata + @"\sharploader\sharploader.log"))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[FEHLER] Konnte die Datei sharploader.log nicht finden...");
                                Console.ResetColor();
                            }
                            else
                            {
                                File.Move(klassen.filesystem.appdata + @"\sharploader\sharploader.log", klassen.filesystem.appdata + @"\sharploader_backup\sharploader.log");
                            }

                            //error.log
                            if (!File.Exists(klassen.filesystem.appdata + @"\sharploader\error.log"))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[FEHLER] Konnte die Datei svn.log nicht finden...");
                                Console.ResetColor();
                            }
                            else
                            {
                                File.Move(klassen.filesystem.appdata + @"\sharploader\svn.log", klassen.filesystem.appdata + @"\sharploader_backup\error.log");
                            }

                        }
                        else
                        {
                            //svn.log:
                            if (!File.Exists(klassen.filesystem.appdata + @"\sharploader\svn.log"))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[FEHLER] Konnte die Datei svn.log nicht finden...");
                                Console.ResetColor();
                            }
                            else
                            {
                                File.Move(klassen.filesystem.appdata + @"\sharploader\svn.log", klassen.filesystem.appdata + @"\sharploader_backup\svn.log");
                            }

                            //error.log
                            if (!File.Exists(klassen.filesystem.appdata + @"\sharploader\error.log"))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[FEHLER] Konnte die Datei svn.log nicht finden...");
                                Console.ResetColor();
                            }
                            else
                            {
                                File.Move(klassen.filesystem.appdata + @"\sharploader\svn.log", klassen.filesystem.appdata + @"\sharploader_backup\error.log");
                            }
                            //update.log
                            if (!File.Exists(klassen.filesystem.appdata + @"\sharploader\update.log"))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[FEHLER] Konnte die Datei svn.log nicht finden...");
                                Console.ResetColor();
                            }
                            else
                            {
                                File.Move(klassen.filesystem.appdata + @"\sharploader\svn.log", klassen.filesystem.appdata + @"\sharploader_backup\update.log");
                            }
                            //fail.txt
                            if (!File.Exists(klassen.filesystem.appdata + @"\sharploader\fail.txt"))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[FEHLER] Konnte die Datei svn.log nicht finden...");
                                Console.ResetColor();
                            }
                            else
                            {
                                File.Move(klassen.filesystem.appdata + @"\sharploader\svn.log", klassen.filesystem.appdata + @"\sharploader_backup\fail.txt");
                            }
                            //Hash.txt
                            if (!File.Exists(klassen.filesystem.appdata + @"\sharploader\hash.txt"))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[FEHLER] Konnte die Datei hash.txt nicht finden...");
                                Console.ResetColor();
                            }
                            else
                            {
                                File.Move(klassen.filesystem.appdata + @"\sharploader\hash.txt", klassen.filesystem.appdata + @"\sharploader_backup\hash.txt");
                            }
                            //version
                            if (!File.Exists(klassen.filesystem.appdata + @"\sharploader\version.txt"))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[FEHLER] Konnte die Datei hash.txt nicht finden...");
                                Console.ResetColor();
                            }
                            else
                            {
                                File.Move(klassen.filesystem.appdata + @"\sharploader\version.txt", klassen.filesystem.appdata + @"\sharploader_backup\version.txt");
                            }
                        }

                        break;
                    case "n":
                        if (File.Exists(klassen.filesystem.appdata + @"\sharploader\svn.log"))
                            File.Delete(klassen.filesystem.appdata + @"\sharploader\svn.log");
                        if (File.Exists(klassen.filesystem.appdata + @"\sharploader\error.log"))
                            File.Delete(klassen.filesystem.appdata + @"\sharploader\error.log");
                        if (File.Exists(klassen.filesystem.appdata + @"\sharploader\hash.log"))
                            File.Delete(klassen.filesystem.appdata + @"\sharploader\hash.log");
                        if (File.Exists(klassen.filesystem.appdata + @"\sharploader\cmd.txt"))
                            File.Delete(klassen.filesystem.appdata + @"\sharploader\cmd.txt");
                        if (File.Exists(klassen.filesystem.appdata + @"\sharploader\update.log"))
                            File.Delete(klassen.filesystem.appdata + @"\sharploader\update.log");
                        if (File.Exists(klassen.filesystem.appdata + @"\sharploader\fail.txt"))
                            File.Delete(klassen.filesystem.appdata + @"\sharploader\fail.txt");
                        if (File.Exists(klassen.filesystem.appdata + @"\sharploader\hash.txt"))
                            File.Delete(klassen.filesystem.appdata + @"\sharploader\hash.txt");
                        if (File.Exists(klassen.filesystem.appdata + @"\sharploader\version.txt"))
                            File.Delete(klassen.filesystem.appdata + @"\sharploader\version.txt");
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                klassen.logger._logger(1, ex.Message);
            }
            
        }
        
        //Imported from uploader.cs
        /// <summary>
        /// Diese Methode sucht nach dem gegebenen FTP Server. 
        /// </summary>
        /// <param name="ftpserver"></param>
        public static void searchforgivenftpserver(string ftpserver)
        {
            Console.WriteLine("We will now search for the given FTP Server!");

            IPHostEntry host;
            host = Dns.GetHostEntry(ftpserver);

            foreach (IPAddress ip in host.AddressList)
            {
                klassen.logger.__logger("\t1", "\tHost Entry for FTP Server: " + ftpserver + " is " + ip);
                Ping sender = new Ping();
                PingOptions opt = new PingOptions();
                opt.DontFragment = true;

                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 120;

                PingReply rep = sender.Send(ip, timeout, buffer, opt);

                if (rep.Status == IPStatus.Success)
                {
                    Console.WriteLine("Sucess, FTP Server found at {0}   ", ftpserver);
                    klassen.logger.__logger("\t1", "\tFound FTP Server " + ftpserver + " on " + ip);

                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Failed: {0}", rep.Status);
                }

            }
        }


        public static void doMT()
        {
            MessageBox.Show("Danke das Sie sich für die MultiThreading Version des Uploaders interessieren. Doch leider ist die Funktion noch in der Planung und der switch -MT"
                            + " wird bis auf weiteres nur diese Meldung hier ausgeben! Bitte starten Sie das Programm ohne -MT!", "Danke für Ihr Interesse", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            klassen.logger.__logger("1", "MT wird noch nicht unterstützt!");
            Environment.Exit(-1);
        }

        public static void gibwasaus()
        {
            Console.WriteLine("Ich bin ein Test!");
            Console.ReadLine();

        }

        public static void doUpdateoServer()
        {
            WebClient _update = new WebClient();
            
        }

        public static void showversion()
        {
            string sharpuploader = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            Console.WriteLine(sharpuploader);
            Console.ReadLine();
        }


        /// <summary>
        /// Vergleicht die gegebene Version mit der Version von files.themadbrainz.net
        /// </summary>
        public static void versioncheck()
        {
            string sharpuploader = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            WebClient client = new WebClient();
            client.DownloadFile("https://files.themadbrainz.net:446/version.txt", klassen.filesystem.appdata + @"\tmbsvn\version.txt");


            StreamReader _version = new StreamReader(klassen.filesystem.appdata + @"\tmbsvn\version.txt");
            string inhalt = _version.ReadToEnd();

            if (sharpuploader == inhalt)
            {
                Console.WriteLine("Kein Update nötig! Uploader Version: " + sharpuploader + " Letzte Version: " + inhalt);
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine(" Update nötig! Uploader Version: " + sharpuploader + " Letzte Version: " + inhalt);
                Console.ReadLine();
            }

        }

        public static void splitfile(int chunk, string file)
        {
            Console.WriteLine("Split v.0.1");


            try
            {
                string path = " ";
                Console.WriteLine("Geben Sie einen Ausgabepfad ein: ");
                path = Console.ReadLine();

                Console.WriteLine("Splitte nun die angegebene Datei {0}!", file);



                if (file == string.Empty && chunk == 0 && path == string.Empty)
                {
                    klassen.logger._logger(1, "Es wurde für die SplitMethode kein Filename, Chunk und kein Pfad gegeben!");
                }
                else
                {
                    byte[] buffer = new byte[chunk];

                    using (Stream input = File.OpenRead(file))
                    {
                        short index = 0;
                        while (input.Position < input.Length)
                        {
                            using (Stream output = File.Create(path + "\\" + index + ".rar"))
                            {
                                int chunkBytesRead = 0;
                                while (chunkBytesRead < chunk)
                                {
                                    GC.Collect();
                                    int bytesRead = input.Read(buffer,
                                                               chunkBytesRead,
                                                               chunk - chunkBytesRead);

                                    if (bytesRead == 0)
                                    {
                                        break;
                                    }
                                    chunkBytesRead += bytesRead;
                                    
                                }
                                output.Write(buffer, 0, chunkBytesRead);
                            }
                            index++;



                        }
                        Console.WriteLine("Es wurden {0} Parts mit 250mb Chunks gesplittet!", index);
                        klassen.logger.__logger("\t1", "Es wurden " + index + " Parts mit 250mb Chunks gesplittet!");

                    }
                }
            }
            catch (Exception ex)
            {
                klassen.logger._logger(1, ex.Message);
                klassen.logger._logger(1, ex.StackTrace);
            }
        }

        public static void createfileforautologin()
        {

            Stopwatch watch05 = new Stopwatch();

            try
            {
                watch05.Start();
                string user, password, ftpserver;
                string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                Console.WriteLine("\t********************************************");
                Console.WriteLine("\tWelcome to the Autologin for the Sharploader");
                Console.WriteLine("\t********************************************");

                Console.WriteLine("\tTo Provide Autologin we need a Valid Username");
                Console.WriteLine("\tA Valid Password, and, of course, an FTP Server!");
                Console.WriteLine("\tIn this early Stage, the Data is NOT encrypted!");
                Console.WriteLine("\tWe will provide this in the Full Version!");
                Console.WriteLine("\tAnonymus Autologin will not be provide!");

                Console.WriteLine("Username: ");
                user = Console.ReadLine();

                Console.WriteLine("Password: ");
                Console.ForegroundColor = ConsoleColor.Black;
                password = Console.ReadLine();
                Console.ResetColor();

                Console.WriteLine("FTP Server: ");
                ftpserver = Console.ReadLine();

                if (user == string.Empty && password == string.Empty && ftpserver == string.Empty)
                {
                    Console.WriteLine("WE WILL DO NOT PROVIDE ANY ANONYMUS AUTOLOGIN!");
                    klassen.logger.__logger("1", "No FTP User given... aborting..");
                }
                else
                {
                    StreamWriter _autologin = new StreamWriter(appdata + @"\sharploader\autologin.login", true, ASCIIEncoding.UTF8, 12);
                    _autologin.WriteLine(user + ",");
                    _autologin.WriteLine(password + ",");
                    _autologin.WriteLine(ftpserver);
                    _autologin.Close();
                    _autologin.Flush();

                    Console.Clear();

                }

                watch05.Stop();
                klassen.logger.__logger("\t1\t", "Used Time by the AUTOLOGIN Method: " + watch05.Elapsed);
            }
            catch (Exception ex)
            {
                klassen.logger._logger(3, ex.Message);
                watch05.Stop();
                klassen.logger.__logger("\t1\t", "Used Time by the AUTOLOGIN Method (SUB EXCPETION:" + ex.Message + ": " + watch05.Elapsed);
            }
        }

        public static void deletegivefileonftp()
        {
            try
            {


                string user, password, datei;
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

                Console.WriteLine("Datei: ");
                datei = Console.ReadLine();

                FileInfo _dateiinfo = new FileInfo(datei);


                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create("ftp://" + ftpserver + "/" + datei);
                request.Credentials = new NetworkCredential(user, password);
                
                request.EnableSsl = true;

                if (_dateiinfo.Extension != string.Empty)
                {
                    request.Method = WebRequestMethods.Ftp.DeleteFile;
                }
                else
                {
                    request.Method = WebRequestMethods.Ftp.RemoveDirectory;
                }

                FtpWebResponse respone = (FtpWebResponse)request.GetResponse();

                int statuscode = (int)respone.StatusCode;

                Console.WriteLine("Last Server Respone (Respone from: " + ftpserver + ")");
                Console.WriteLine(respone.StatusDescription + "(" + respone.StatusCode + ")");

                klassen.logger.__logger("\t1", "File Delete Operation successfull, Last Respone: " + respone);
                klassen.logger.__logger("\t1", "File: " + _dateiinfo.FullName);
                klassen.logger.__logger("\t1", "Lenght: " + _dateiinfo.Length);
            }
            catch (Exception ex)
            {
                klassen.logger.__logger("1", ex.Message);
                klassen.logger.__logger("1", ex.StackTrace);


            }
            


        }


        
    }
}
