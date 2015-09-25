using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankConsole
{
    class Greeter
    {
        /// <summary>
        /// this program determins the main code path that the code will follow
        /// </summary>
        /// <returns></returns>
        public static bool RunProgram()
        {
            // generate the welcome text
            GetWelcomeString();

            // get the user input for the options
            GetUserInput();

            // user decides if they would like to continue
            return GetContinueNotification();
        }

        /// <summary>
        /// this offers a little bit of validation on if they want to continue or not
        /// </summary>
        /// <returns></returns>
        private static bool GetContinueNotification()
        {
            Console.WriteLine("\nWould you like to continue? (Y/N)");
            ConsoleKeyInfo userInput = Console.ReadKey();
            
            try
            {
                String str = String.Format("{0}", userInput.KeyChar);
                if(str.Equals("Y",StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                
                if(str.Equals("N",StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }

                Console.WriteLine("\nThe input you selected is invalid. Please try again...");
                return GetContinueNotification();
            }
            catch
            {
                Console.WriteLine("\nThe input you selected is invalid. Please try again...");
                return GetContinueNotification();
            }
        }

        private static void GetUserInput()
        {
            
        }

        /// <summary>
        /// this method generates the welcome string for the new user
        /// </summary>
        /// <returns></returns>
        private static void GetWelcomeString()
        {
            // this will be used to construct the welcome prompt
            StringBuilder text = new StringBuilder("\n\n");

            // here we adjust the greeting
            TimeSpan current = DateTime.Now.TimeOfDay;
            if (current < new TimeSpan(10, 30, 00))
            {
                text.Append("Good morning,");
            }
            else if (current < new TimeSpan(10, 30, 00))
            {
                text.Append("Hello,");
            }
            else if (current < new TimeSpan(13, 0, 0))
            {
                text.Append("Good afternoon,");
            }
            else
            {
                text.Append("Good evening,");
            }
            text.AppendLine(" and welcome to the First Third Bank.");

            Console.WriteLine(text.ToString());
        }
    }
}
