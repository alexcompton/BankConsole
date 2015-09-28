using Portals;
using PseudoDatabase;

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
        /// this declaration and constructor allow us to simulate a database
        /// </summary>
        private static Database pseudoDatabase;
        public Greeter()
        {
            pseudoDatabase = new Database();
        }

        /// <summary>
        /// this program determins the main code path that the code will follow
        /// </summary>
        /// <returns></returns>
        public bool RunProgram()
        {
            // generate the welcome text
            GetWelcomeString();

            // get the user input for the options
            GetUserInfo();

            // user decides if they would like to continue
            return GetContinueNotification();
        }

        /// <summary>
        /// this is where we decide who the user is (customer or banker)
        /// </summary>
        private static void GetUserInfo()
        {
            Console.WriteLine("\nPlease identify your relation with First Third Bank.\n"
                +"This will help us better serve you:"+
                "\n\n  1) Customer\n  2) Banker\n");
            ConsoleKeyInfo userInput = Console.ReadKey();

            try
            {
                switch(int.Parse(userInput.KeyChar.ToString()))
                {
                    case 1:
                        GetCustomerInfo(0);
                        break;
                    case 2:
                        GetBankerInfo(0);
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
                if (str.Equals("Y", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }

                if (str.Equals("N", StringComparison.OrdinalIgnoreCase))
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
        /// this is the banker portal
        /// </summary>
        private static void GetBankerInfo(int count)
        {
            if (count > 2)
            {
                Console.WriteLine("\n\nYou have entered an incorrect password 3 times. Goodbye.\n");
                return;
            }

            // prompt for the password
            Console.WriteLine("\n\nWe need to verify your identity to access the First Third Banker Portal."
                +"\nPlease enter your user credentials:\n(Hint: banker)\n");
            String str = Console.ReadLine();

            // compare the password if it matches start the portal otherwise try again
            if (str.Equals("banker", StringComparison.OrdinalIgnoreCase))
            {
                Banker banker = new Banker(pseudoDatabase);
                banker.StartPortal();
            }
            else
            {
                Console.WriteLine("\n\nYou have entered and incorrect password."
                    + "\nYou have " + (2 - count) + " more chances to enter the correct password.");
                GetBankerInfo(++count);
            }
        }

        private static void GetCustomerInfo(int count)
        {
            if (count > 2)
            {
                Console.WriteLine("\n\nYou have entered an incorrect password 3 times. Goodbye.\n");
                return;
            }

            // prompt for the password
            Console.WriteLine("\n\nWe need to verify your identity to access the First Third customer Portal."
                +"\nPlease enter your account number:\n(Hint: 10001 - 10050)\n");
            String str = Console.ReadLine();
            int accountNumber = int.Parse(str);

            // compare the password if it matches start the portal otherwise try again
            if (accountNumber > 10000 && accountNumber < 10051)
            {
                Customer customer = new Customer(accountNumber,pseudoDatabase);
                customer.StartPortal();
                pseudoDatabase = customer.GetPseudoDatabase();
            }
            else
            {
                Console.WriteLine("\n\nYou have entered and incorrect password."
                    + "\nYou have " + (2 - count) + " more chances to enter the correct password.");
                GetCustomerInfo(++count);
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
