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
    class upload
    {
        //Änderung TFS
        public static void suploader()
        {
            try
            {
                if (File.Exists(repouploader.klassen.filesystem.appdata + @"\sharploader\autologin.login"))
                {
                    klassen.logger.__logger("1", "AUTOLOGIN gefunden...");

                    //string inhalt = " ";
                    StreamReader _autologin = new StreamReader(klassen.filesystem.appdata + @"\shrarploader\autologin.login");

                    ArrayList logininformationen = new ArrayList(3);
                    string sLine = " ";


                    while (sLine != null)
                    {
                        sLine = _autologin.ReadLine();
                        if (sLine != null)
                            logininformationen.Add(sLine);
                    }

                    foreach (string output in logininformationen)
                    {
                        Console.WriteLine(output);
                        Console.ReadLine();


                    }

                }
                else
                {
                    string user, password, file;
                    string ftpserver = " ";


                    Console.WriteLine(" Benötige nun die Anmeldedaten und die hoch zu ladene Datei!");
                    Console.WriteLine(" User:");
                    user = Console.ReadLine();

                    Console.WriteLine(" Password: ");
                    Console.ForegroundColor = ConsoleColor.Black;
                    password = Console.ReadLine();
                    Console.ResetColor();

                    Console.WriteLine(" Pfad zur Datei: ");
                    file = Console.ReadLine();

                    Console.WriteLine("FTP Server (Nur die korrekte Uri OHNE ftp://");
                    ftpserver = Console.ReadLine();

                    FileInfo _uploadfile = new FileInfo(file);
                    string name = _uploadfile.Name;
                    string ext = _uploadfile.Extension;
                    long groesse = _uploadfile.Length;
                    //int groesse = _uploadfile.Length;




                    switch (_uploadfile.Extension)
                    {
                        case ".exe":
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Aufgrund von Sicherheitsbestimmungen ist es nicht möglich Ausführbare Dateien hochzuladen!");
                            Console.ResetColor();

                            klassen.logger.__logger("1", "exe Datei erkannt, hochladen aborted!");
                            repouploader.uploader.init();
                            break;
                        case ".bat":
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Aufgrund von Sicherheitsbestimmungen ist es nicht möglich Ausführbare Dateien hochzuladen!");
                            Console.ResetColor();

                            klassen.logger.__logger("1", "bat Datei erkannt, hochladen aborted!");
                            repouploader.uploader.init();
                            break;
                        case ".cmd":
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Aufgrund von Sicherheitsbestimmungen ist es nicht möglich Ausführbare Dateien hochzuladen!");
                            klassen.logger.__logger("1", "cmd Datei erkannt, hochladen aborted!");
                            Console.ResetColor();
                            repouploader.uploader.init();
                            break;
                        default:
                            break;
                    }



                    if (_uploadfile.Length > 1000000000)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Datei mit über 1GB Größe gefunden, split wird aufgerufen!");
                        klassen.logger.__logger("1", "1GB Datei gefunden, rufe nun die split methode auf..");
                        Console.ResetColor();

                        dev.devfun.splitfile(250000000, file);

                        Console.ResetColor();
                        Console.ReadLine();
                    }
                    else
                    {

                        klassen.logger.__logger("\t1", "\tFolgende Datei wird hochgeladen: " +
                                                                            "\tDateiname: " + name +
                                                                            "\tGröße: " + groesse +
                                                                            "\tVerzeichniss: " + _uploadfile.DirectoryName);


                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + ftpserver + "/" + name);


                        request.Method = WebRequestMethods.Ftp.UploadFile;
                        request.Credentials = new NetworkCredential(user, password);
                        request.EnableSsl = true;
                        klassen.logger.__logger("\t1\t", "IsSSL: true");

                        FtpWebResponse ftprespone = (FtpWebResponse)request.GetResponse();
                        Console.WriteLine("Versuche nun die Datei {0} hoch zu laden...", name);
                        Console.WriteLine("\r\n");
                        Console.WriteLine("----------------------------------------------");
                        Console.WriteLine("Dateiinformationen: ");
                        Console.WriteLine("Dateiname: " + name);
                        Console.WriteLine("Größe: " + groesse);
                        Console.WriteLine("Verzeichniss: " + _uploadfile.DirectoryName);
                        Console.WriteLine("Tatsächliche Größe: " + _uploadfile.Length);
                        Console.WriteLine("----------------------------------------------");
                        Console.WriteLine("\r\n");

                        string welcomemessage = ftprespone.WelcomeMessage;
                        Console.WriteLine(welcomemessage);

                        StreamReader _sourcestream = new StreamReader(file);
                        byte[] filecontens = Encoding.UTF8.GetBytes(_sourcestream.ReadToEnd());
                        _sourcestream.Close();
                        request.ContentLength = file.Length;

                        Stream requestStream = request.GetRequestStream();
                        requestStream.Write(filecontens, 0, filecontens.Length);

                        requestStream.Close();


                        Console.Clear();

                        klassen.logger.__logger("\t1\t", "\tUpload der Datei " + name + " mit der Grösse: " + groesse + " wurde erfolgreich hochgeladen!");
                        klassen.logger.__logger("\t1\t", ftprespone.StatusDescription + "(" + ftprespone.StatusCode + ")");

                        if (ftprespone.StatusCode == FtpStatusCode.FileActionAborted)
                        {
                            Console.WriteLine("Fehler, der FTP Server konnte die Aktion nicht ausführen! Lesen Sie die Fehlermeldung unter");
                            Console.WriteLine(@"%APPDATA%\tmbsvn\error.log oder fragen Sie ihren Systemadministrator!");

                            Console.WriteLine("Fehlercode: " + ftprespone.StatusCode);
                        }
                        else
                        {

                            Console.WriteLine("Letzter Status: {0}", ftprespone.StatusDescription);
                        }

                    }
                }





            }
            catch (Exception ex)
            {
                klassen.logger._logger(1, ex.Message);
                klassen.logger._logger(1, ex.StackTrace);
                klassen.logger.__logger("1", "Fehler gefunden, führe nun weitere Schritte aus!");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Letzter Fehler: {0} ", ex.Message);
                Console.ResetColor();
                Process.Start(@"C:\\Program Files\\Internet Explorer\\IExplore.exe", "https://files.themadbrainz.net:446/sharploader/Support");
                GC.Collect();
                klassen.logger.__logger("\t1", "GC ate some Memory, *nom*");

            }

        }

        public static void uploader()
        {
            Stopwatch watch05 = new Stopwatch();

            try
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("If your FTP Server supports TLS / SSL please use: up -ssl instead!");
                Console.ResetColor();


                watch05.Start();

                if (!File.Exists(klassen.filesystem.appdata + @"\tmbsvn\autologin.login"))
                {

                    string user, password, file;
                    string ftpserver = " ";
                    Console.WriteLine(" Benötige nun die Anmeldedaten und die hoch zu ladene Datei!");
                    Console.WriteLine(" User:");
                    user = Console.ReadLine();

                    Console.WriteLine(" Password: ");
                    Console.ForegroundColor = ConsoleColor.Black;
                    password = Console.ReadLine();
                    Console.ResetColor();

                    Console.WriteLine(" Pfad zur Datei: ");
                    file = Console.ReadLine();

                    Console.WriteLine("FTP Server (Nur die korrekte Uri OHNE ftp://");
                    ftpserver = Console.ReadLine();




                    //Try One to Fix the Bug #0000014 (Thanks to Menthe / Equitas)
                    //http://themadbrainz.net/bugtracker/view.php?id=14
                    if (!File.Exists(file))
                    {
                        Console.WriteLine("Kann die Datei {0}, nicht finden...", file);
                        Console.ReadLine();

                        klassen.logger.__logger("\t1", "Konnte die Datei " + file + " nicht hochladen, Grund: Datei wurde nicht gefunden!");
                    }
                    else
                    {

                        if (ftpserver.StartsWith("ftp://"))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Upload abgebrochen, bitte den Servernamen OHNE ftp angeben!");
                            Console.ResetColor();

                            klassen.logger.__logger("\t1", "UPLOAD Method aborted, ftp:// detected!");
                        }
                        else
                        {



                            FileInfo _uploadfile = new FileInfo(file);
                            string name = _uploadfile.Name;
                            string ext = _uploadfile.Extension;
                            long groesse = _uploadfile.Length;

                            klassen.logger.__logger("\t1", "\tFolgende Datei wird hochgeladen: " +
                                                                                "\tDateiname: " + name +
                                                                                "\tGröße: " + groesse +
                                                                                "\tVerzeichniss: " + _uploadfile.DirectoryName);

                            switch (_uploadfile.Extension)
                            {
                                case ".exe":
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Aufgrund von Sicherheitsbestimmungen ist es nicht möglich Ausführbare Dateien hochzuladen!");
                                    Console.ResetColor();

                                    klassen.logger.__logger("1", "exe Datei erkannt, hochladen aborted!");
                                    repouploader.uploader.init();
                                    break;
                                case ".bat":
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Aufgrund von Sicherheitsbestimmungen ist es nicht möglich Ausführbare Dateien hochzuladen!");
                                    Console.ResetColor();

                                    klassen.logger.__logger("1", "bat Datei erkannt, hochladen aborted!");
                                    repouploader.uploader.init();
                                    break;
                                case ".cmd":
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Aufgrund von Sicherheitsbestimmungen ist es nicht möglich Ausführbare Dateien hochzuladen!");
                                    klassen.logger.__logger("1", "cmd Datei erkannt, hochladen aborted!");
                                    Console.ResetColor();
                                    repouploader.uploader.init();
                                    break;
                                default:
                                    break;
                            }

                            if (_uploadfile.Length > 1000000000)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Dateien die über 1GB groß sind bitte vorher splitten und dann hoch laden!");
                                Console.WriteLine("Benutzen Sie dafür einfach den Befehl split!");
                                Console.ResetColor();
                                Console.ReadLine();
                            }
                            else
                            {



                                FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + ftpserver + "/" + name);

                                ServicePoint service = request.ServicePoint;
                                klassen.logger.__logger("\t1", "\tDerzeit erlaubte FTP Connections: " + service.ConnectionLimit);
                                service.ConnectionLimit = 1;

                                FtpWebResponse ftprespone = (FtpWebResponse)request.GetResponse();
                                
                                if(ftprespone.StatusDescription == FtpStatusCode.NotLoggedIn.ToString())
                                {
                                    if (ftprespone.StatusCode == FtpStatusCode.ServerWantsSecureSession)
                                    {
                                        //string ssl_antwort = " ";
                                        Console.WriteLine("Serververbindung erfordert eine SSL Verbindung an...");
                                        Console.WriteLine("Wechsle nun zum SSL Upload");

                                        suploader();

                                    }
                                    else
                                    {

                                        request.Method = WebRequestMethods.Ftp.UploadFile;
                                        request.Credentials = new NetworkCredential(user, password);
                                        //request.EnableSsl = true;
                                        Console.WriteLine("Versuche nun die Datei {0} hoch zu laden...", name);
                                        StreamReader _sourcestream = new StreamReader(file);
                                        byte[] filecontens = Encoding.UTF8.GetBytes(_sourcestream.ReadToEnd());
                                        _sourcestream.Close();
                                        request.ContentLength = file.Length;

                                        Stream requestStream = request.GetRequestStream();
                                        requestStream.Write(filecontens, 0, filecontens.Length);
                                        requestStream.Close();


                                        Console.Clear();

                                        klassen.logger.__logger("\t1\t", "\tUpload der Datei " + name + " mit der Grösse: " + groesse + " wurde erfolgreich hochgeladen!");
                                        klassen.logger.__logger("\t1\t", ftprespone.StatusDescription + "(" + ftprespone.StatusCode + ")");

                                        if (ftprespone.StatusCode == FtpStatusCode.FileActionAborted)
                                        {
                                            Console.WriteLine("Fehler, der FTP Server konnte die Aktion nicht ausführen! Lesen Sie die Fehlermeldung unter");
                                            Console.WriteLine(@"%APPDATA%\tmbsvn\error.log oder fragen Sie ihren Systemadministrator!");

                                            Console.WriteLine("Fehlercode: " + ftprespone.StatusCode);
                                        }
                                        else
                                        {

                                            Console.WriteLine("Letzter Status: {0}", ftprespone.StatusDescription);

                                            watch05.Stop();
                                            klassen.logger.__logger("\t1\t", "Used Time by the UPLOAD Mehtod: " + watch05.Elapsed);
                                        }
                                    }
                                }

                                
                            }
                        }

                    }
                }
                else
                {

                    Console.WriteLine(@"Bitte löschen Sie unter %APPDATA%\tmbsvn die autologin.login!");
                    Console.ReadLine();


                }

            }
            catch (Exception ex)
            {
                klassen.logger._logger(1, ex.Message);
                klassen.logger._logger(1, ex.StackTrace);


                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Letzter Fehler: {0} ", ex.Message);
                Console.ResetColor();
                Console.ReadLine();
            }

        }
    }
}
