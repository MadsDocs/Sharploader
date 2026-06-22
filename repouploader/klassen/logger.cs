using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;


namespace repouploader.klassen
{
    class logger
    {

        public static void _logger(short ID, string message)
        {
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            StreamWriter _error = new StreamWriter(appdata + @"\sharploader\error.log", true, ASCIIEncoding.UTF8, 12);
            _error.WriteLine("\t["+DateTime.Now +"]" + ID.ToString() + "\tBeschreibung: " + message);
            _error.Flush();
            _error.Close();
        }

        public static void __logger(string ID, string message)
        {

            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            StreamWriter _log = new StreamWriter(appdata + @"\sharploader\sharploader.log", true, ASCIIEncoding.UTF8, 12);
            //_log.WriteLine("                                                                   ");
            _log.WriteLine("\t[" + DateTime.Now + "]"  +  ID + "\tBeschreibung: " + message);
            _log.Flush();
            _log.Close();
        }

        public static void _dummy(string dummymessage)
        {
            if (!File.Exists(filesystem.appdata + @"\sharploader\dummy.log"))
            {
                StreamWriter _dummy = new StreamWriter(filesystem.appdata + @"\sharploader\dummy.log", true, ASCIIEncoding.UTF8, 12);
                _dummy.WriteLine("\t[" + DateTime.Now + "]" + "\tBeschreibung: " + dummymessage);
                _dummy.Flush();
                _dummy.Close();
            }
            else
            {
                logger.__logger("\t1", "Dummy Datei ist vorhanden, Init nicht nötig!");
            }
        }

        public static void _updatelog(short ID, string Message, string updatecode)
        {
            StreamWriter _updatelog = new StreamWriter(filesystem.appdata + @"\sharploader\update.log", true, ASCIIEncoding.UTF8, 12);
            _updatelog.WriteLine("\t[" + DateTime.Now + "]" + ID + Message + updatecode);
            _updatelog.Flush();
            _updatelog.Close();
        }
    }
}
