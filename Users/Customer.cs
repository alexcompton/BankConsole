using PseudoDatabase;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portals
{
    public class Customer : User
    {
        private int accountNumber;
        private Account account;
        private Database pseudoDatabase;
        public Database GetPseudoDatabase() { return this.pseudoDatabase; }
        public Customer(int accountNumber, Database pseudoDatabase)
        {
            this.accountNumber = accountNumber;
            this.account = pseudoDatabase.GetData()[accountNumber];
            this.pseudoDatabase = pseudoDatabase;
        }

        /// <summary>
        /// this is the menu options
        /// </summary>
        /// <returns></returns>
        protected override bool RunPortal()
        {
            Console.WriteLine("\nHello " + account.GetUserName() + ", how may I help you?" +
                "\n\n  1) Get your current balance." +
                "\n  2) Get you balance at a particular date." +
                "\n  3) View your last five transactions." +
                "\n  4) Get a list of the five customers with the largest balance." +
                "\n  5) Get a list of the five customers with the smallest balance." +
                "\n  6) Exit.\n");
            ConsoleKeyInfo userInput = Console.ReadKey();

            try
            {
                switch (int.Parse(userInput.KeyChar.ToString()))
                {
                    case 1:
                        GetCurrentBalance();
                        return true;
                    case 2:
                        GetBalanceAtDate();
                        return true;
                    case 3:
                        GetLastTransactions(5);
                        return true;
                    case 4:
                        return true;
                    case 5:
                        return true;
                    case 6:
                        return false;
                    default:
                        Console.WriteLine("\nThe input you selected is invalid. Please try again...");
                        return true;
                }
            }
            catch
            {
                Console.WriteLine("\nCouldn't process your selection, please try again...");
                return true;
            }
        }

        /// <summary>
        /// this method will get the last five trasactions of a customer but,
        /// it can display how ever many you might need
        /// </summary>
        /// <param name="numberOfTransactions">Int</param>
        protected override void GetLastTransactions(int numberOfTransactions)
        {
            // now if this is a valid account number then we get the correct account
            // in a human readable format
            Console.WriteLine("\nThese are the last " + numberOfTransactions
                + " transactions for account number: " + this.accountNumber + "\n"
                + this.account.PrintLastTransactions(numberOfTransactions));
        }

        /// <summary>
        /// this will get the current balance of an account
        /// </summary>
        protected override void GetCurrentBalance()
        {
            // now if this is a valid account number then we get the correct account
            // in a human readable format
            Console.WriteLine("\nAccount balance for account number: " + this.accountNumber + "\n"
                + this.account.PrintBalance());
        }

        /// <summary>
        /// gets the value of an account at a certain date
        /// </summary>
        protected override void GetBalanceAtDate()
        {
            // find the balance at this date
            DateTime date = GetDateFromUser(this.accountNumber);
            if (DateTime.Compare(date, new DateTime(1980, 1, 1)) < 0) return;

            // now if this is a valid account number then we get the correct account
            // in a human readable format
            Console.WriteLine("\nAccount balance for account number: " + this.accountNumber + "\n"
                + this.account.PrintBalance(date));
        }
    }
}
