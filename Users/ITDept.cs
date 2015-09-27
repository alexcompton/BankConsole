using PseudoDatabase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Portals
{
    public class ITDept
    {
        /// <summary>
        /// this actually just runs the portal until the user decides to leave
        /// </summary>
        public static void StartITPortal()
        {
            Console.WriteLine("\n\nWelcome to the First Third IT Department Portal.\n");
            while (RunPortal()) { }
        }

        /// <summary>
        /// this is the menu options
        /// </summary>
        /// <returns></returns>
        private static bool RunPortal()
        {
            Console.WriteLine("\nWhat would you like to accomplish?" +
                "\n\n  1) Download Data\n  2) Upload Data\n  3) Exit");
            ConsoleKeyInfo userInput = Console.ReadKey();

            try
            {
                switch (int.Parse(userInput.KeyChar.ToString()))
                {
                    case 1:
                        DownloadDataMenu();
                        return true;
                    case 2:
                        UploadDataMenu();
                        return true;
                    case 3:
                        return false;
                    default:
                        Console.WriteLine("\nThe input you selected is invalid. Please try again...");
                        return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nCouldn't process your selection, please try again..." + ex.ToString());
                return true;
            }
        }

        /// <summary>
        /// this will allow the user to export a snapshot of the data to an xml file (mostly used for testing)
        /// </summary>
        private static void DownloadDataMenu()
        {
            Database data = new Database();
            data.ToString();
        }

        /// <summary>
        /// this will allow the user to import a snapshot of the data to an xml file (mostly used for testing)
        /// </summary>
        private static void UploadDataMenu()
        {
            Console.WriteLine("upload data");
        }
    }
}
