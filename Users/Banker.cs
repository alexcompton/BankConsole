using PseudoDatabase;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portals
{
    public class Banker : User
    {
        /// <summary>
        /// this declaration and constructor allow us to simulate a database
        /// </summary>
        private Database pseudoDatabase;
        public Banker(Database pseudoDatabase)
        {
            this.pseudoDatabase = pseudoDatabase;
        }

        /// <summary>
        /// this is the menu options
        /// </summary>
        /// <returns></returns>
        protected override bool RunPortal()
        {
            Console.WriteLine("\nWhat would you like to accomplish?" +
                "\n\n  1) Get the current balance of a customer." +
                "\n  2) Get the balance of a customer at a particular date." +
                "\n  3) View last five transactions of a customer." +
                "\n  4) Get a list of the five customers with the largest balance." +
                "\n  5) Get a list of the five customers with the smallest balance." +
                "\n  6) Exit.\n");
            ConsoleKeyInfo userInput = Console.ReadKey();
            Console.WriteLine();

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
                        GetLastTransactions();
                        return true;
                    case 4:
                        GetTopProducerList();
                        return true;
                    case 5:
                        GetBottomProducerList();
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
        /// right now we hard wired the list to show the number of top produces to be five but it can accept any number
        /// </summary>
        private void GetTopProducerList()
        {
            int requestedAccounts = 5;
            Console.WriteLine(pseudoDatabase.PrintTopAccounts(requestedAccounts));
        }

        /// <summary>
        /// right now we hard wired the list to show the number of top produces to be five but it can accept any number
        /// </summary>
        private void GetBottomProducerList()
        {
            int requestedAccounts = 5;
            Console.WriteLine(pseudoDatabase.PrintBottomAccounts(requestedAccounts));
        }

        /// <summary>
        /// this method will get the last five trasactions of a customer but,
        /// it can display how ever many you might need
        /// </summary>
        /// <param name="numberOfTransactions">Int</param>
        protected override void GetLastTransactions()
        {
            int numberOfTransactions = 5;

            // get the account number
            int accountNumber = GetAccountInfo();
            if (accountNumber == 0) return;

            // now if this is a valid account number then we get the correct account
            // in a human readable format
            Console.WriteLine("\nThese are the last " + numberOfTransactions 
                +" transactions for account number: " + accountNumber + "\n"
                + pseudoDatabase.GetData()[accountNumber].PrintLastTransactions(numberOfTransactions));
        }

        /// <summary>
        /// this will get the current balance of an account
        /// </summary>
        protected override void GetCurrentBalance()
        {
            // get the account number
            int accountNumber = GetAccountInfo();
            if (accountNumber == 0) return;

            // now if this is a valid account number then we get the correct account
            // in a human readable format
            Console.WriteLine("\nAccount balance for account number: " + accountNumber + "\n"
                + pseudoDatabase.GetData()[accountNumber].PrintBalance());
        }

        /// <summary>
        /// this handles the validation to make sure that this is valid account
        /// </summary>
        /// <returns>int accountNumber</returns>
        private int GetAccountInfo()
        {
            // this will hold the new account number
            int accountNumber = 0;
            Console.WriteLine("\nWhat is the number of the account you would like to access?\n"
                + "(Hint: 10001 - 10050)\n");
            string userInput = Console.ReadLine();

            try
            {
                accountNumber = int.Parse(userInput);

                if (accountNumber < 10001 || accountNumber > 10051)
                {
                    accountNumber = 0;
                    Console.WriteLine("\nCouldn't process your selection, please try again...");
                }
            }
            catch
            {
                Console.WriteLine("\nCouldn't process your selection, please try again...");
            }

            return accountNumber;
        }

        /// <summary>
        /// gets the value of an account at a certain date
        /// </summary>
        protected override void GetBalanceAtDate()
        {
            // get the account number
            int accountNumber = GetAccountInfo();
            if (accountNumber == 0) return;

            // find the balance at this date
            DateTime date = GetDateFromUser(accountNumber);
            if (DateTime.Compare(date, new DateTime(1980, 1, 1)) < 0) return;

            // now if this is a valid account number then we get the correct account
            // in a human readable format
            Console.WriteLine("\nAccount balance for account number: " + accountNumber + "\n"
                + pseudoDatabase.GetData()[accountNumber].PrintBalance(date));
        }
    }
}
