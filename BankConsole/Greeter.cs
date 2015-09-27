using Portals;

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
            GetUserInfo();

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

        /// <summary>
        /// this is where we decide who the user is (customer or banker)
        /// </summary>
        private static void GetUserInfo()
        {
            Console.WriteLine("\nPlease identify your relation with First Third Bank.\n"
                +"This will help us better serve you:"+
                "\n\n  1) Customer\n  2) Banker\n  3) IT Dept.");
            ConsoleKeyInfo userInput = Console.ReadKey();

            try
            {
                switch(int.Parse(userInput.KeyChar.ToString()))
                {
                    case 1:
                        GetCustomerInfo();
                        break;
                    case 2:
                        GetBankerInfo();
                        break;
                    case 3:
                        GetITDeptInfo(0);
                        break;
                    default:
                        Console.WriteLine("\nThe input you selected is invalid. Please try again...");
                        GetUserInfo();
                        break;
                }
            }
            catch
            {
                Console.WriteLine("\nThe input you selected is invalid. Please try again...");
                GetUserInfo();
            }
        }

        /// <summary>
        /// this is the banker portal
        /// </summary>
        private static void GetBankerInfo()
        {
            Console.WriteLine("\n\nWelcome to the First Third Banker Portal.");
        }

        private static void GetCustomerInfo()
        {
            Console.WriteLine("\n\nWelcome valued First Third Bank customer.");
        }

        /// <summary>
        /// If the user enters the correct login then they can go to the IT Dept Portal
        /// </summary>
        private static void GetITDeptInfo(int count)
        {
            if(count > 2) 
            {
                Console.WriteLine("\n\nYou have entered an incorrect password 3 times. Goodbye.\n");
                return;
            }

            // prompt for the password
            Console.WriteLine("\n\nWelcome to the First Third IT Portal.\nPlease enter your user credentials:");
            String str = Console.ReadLine();

            // compare the password if it matches start the portal otherwise try again
            if (str.Equals("itdept",StringComparison.OrdinalIgnoreCase))
            {
                ITDept.StartITPortal();
            }
            else
            {
                Console.WriteLine("\n\nYou have entered and incorrect password."
                    + "\nYou have " + (2-count) + " more chances to enter the correct password.");
                GetITDeptInfo(++count);
            }
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
