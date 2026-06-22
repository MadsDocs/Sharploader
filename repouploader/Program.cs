using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace repouploader
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                string lowargs = args[i].ToLower();

                switch (lowargs)
                {
                    case "-rel":
                            dev.devfun.updev();
                        break;
                    case "-mt":
                        dev.devfun.doMT();
                        break;
                    ///<summary>
                    ///Diese Methode überprüft alle Sharploader Daten
                    ///</summary>
                    case "--check":
                        klassen.fehlerbehandlung.checkforallfiles();
                        break;
                    
                    default:
                        break;
                }
            }



            uploader.init();
        }
    }
}
