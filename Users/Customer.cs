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
        private Database pseudoDatabase;
        public Database GetPseudoDatabase() { return this.pseudoDatabase; }
        public Customer(int accountNumber, Database pseudoDatabase)
        {
            this.accountNumber = accountNumber;
            this.pseudoDatabase = pseudoDatabase;
        }

        /// <summary>
        /// this is the menu options
        /// </summary>
        /// <returns></returns>
        protected override bool RunPortal()
        {
            Console.WriteLine("\nHello " + pseudoDatabase.GetData()[accountNumber].GetUserName() + ", how may I help you?" +
                "\n\n  1) Get your current balance." +
                "\n  2) Get you balance at a particular date." +
                "\n  3) View your last five transactions." +
                "\n  4) Withdraw money from your account." +
                "\n  5) Deposit money to your account." +
                "\n  6) Transfer money to another account." +
                "\n  7) Exit.\n");
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
                        Withdraw();
                        return true;
                    case 5:
                        Deposit();
                        return true;
                    case 6:
                        Transfer();
                        return true;
                    case 7:
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
        /// transfers money from this account to another account
        /// </summary>
        private void Transfer()
        {
            // prompt user for the ammount for this transaction
            decimal transactionAmmount = GetAmmount();
            int recivingAccountNumber = GetRecipient();

            //next we validate the ammount and continue the transaction if it passes
            if (NegativeValidation(transactionAmmount) && OverdraftValidation(transactionAmmount) && recivingAccountNumber != 0)
            {
                // set things up so we can override in data base
                SortedDictionary<DateTime, Trasaction> transactionHistory = this.pseudoDatabase.GetData()[accountNumber].GetTransactionHistory();
                decimal newBalance = this.pseudoDatabase.GetData()[accountNumber].GetBalance() - transactionAmmount;
                // here we save the transaction
                transactionHistory.Add(DateTime.Now, new Trasaction(newBalance, transactionAmmount, TransactionType.Deposit, Currency.Dollar));
                this.pseudoDatabase.GetData()[accountNumber] = new Account(this.pseudoDatabase.GetData()[accountNumber].GetUserName(), Currency.Dollar, transactionHistory);

                // now do the same for the other account
                // set things up so we can override in data base
                transactionHistory = this.pseudoDatabase.GetData()[recivingAccountNumber].GetTransactionHistory();
                newBalance = this.pseudoDatabase.GetData()[recivingAccountNumber].GetBalance() + transactionAmmount;
                // here we save the transaction
                transactionHistory.Add(DateTime.Now, new Trasaction(newBalance, transactionAmmount, TransactionType.Deposit, Currency.Dollar));
                this.pseudoDatabase.GetData()[recivingAccountNumber] = new Account(this.pseudoDatabase.GetData()[recivingAccountNumber].GetUserName(), Currency.Dollar, transactionHistory);
            }

            // might as well display the account balance
            GetCurrentBalance();
        }

        /// <summary>
        /// here we prompt the user for the account number reciepient of the funds, returns 0 if there is an error
        /// </summary>
        /// <returns>int</returns>
        private int GetRecipient()
        {
            // prompt the user for input
            Console.WriteLine("\nPlease enter the number of the account that you wish to transfer funds."
                +"\n(Hint: 10001 - 10050)\n");
            string str = Console.ReadLine();

            try
            {
                int recipient = int.Parse(str);
                
                if(recipient > 10000 && recipient < 10051)
                {
                    return recipient;
                }
            }
            catch
            {
                Console.WriteLine("\nUnable to process your selection, please try again\n");
            }

            return 0;
        }

        /// <summary>
        /// adds money to account
        /// </summary>
        private void Deposit()
        {
            // prompt user for the ammount for this transaction
            decimal transactionAmmount = GetAmmount();

            //next we validate the ammount and continue the transaction if it passes
            if (NegativeValidation(transactionAmmount))
            {
                // set things up so we can override in data base
                SortedDictionary<DateTime, Trasaction> transactionHistory = this.pseudoDatabase.GetData()[accountNumber].GetTransactionHistory();
                decimal newBalance = this.pseudoDatabase.GetData()[accountNumber].GetBalance() + transactionAmmount;
                // here we save the transaction
                transactionHistory.Add(DateTime.Now, new Trasaction(newBalance,transactionAmmount,TransactionType.Deposit,Currency.Dollar));
                this.pseudoDatabase.GetData()[accountNumber] = new Account(this.pseudoDatabase.GetData()[accountNumber].GetUserName(), Currency.Dollar, transactionHistory);
            }

            // might as well display the account balance
            GetCurrentBalance();
        }

        /// <summary>
        /// withdraws money from account
        /// </summary>
        private void Withdraw()
        {
            // prompt user for the ammount for this transaction
            decimal transactionAmmount = GetAmmount();

            //next we validate the ammount and continue the transaction if it passes
            if (NegativeValidation(transactionAmmount) && OverdraftValidation(transactionAmmount))
            {
                // set things up so we can override in data base
                SortedDictionary<DateTime, Trasaction> transactionHistory = this.pseudoDatabase.GetData()[accountNumber].GetTransactionHistory();
                decimal newBalance = this.pseudoDatabase.GetData()[accountNumber].GetBalance() - transactionAmmount;
                // here we save the transaction
                transactionHistory.Add(DateTime.Now, new Trasaction(newBalance, transactionAmmount, TransactionType.Deposit, Currency.Dollar));
                this.pseudoDatabase.GetData()[accountNumber] = new Account(this.pseudoDatabase.GetData()[accountNumber].GetUserName(), Currency.Dollar, transactionHistory);
            }

            // might as well display the account balance
            GetCurrentBalance();
        }

        /// <summary>
        /// this checks to see if there is a potential of overdrafting, and stops the transaction if it does
        /// </summary>
        /// <param name="transactionAmmount"></param>
        /// <returns></returns>
        private bool OverdraftValidation(decimal transactionAmmount)
        {
            if(transactionAmmount >= pseudoDatabase.GetData()[accountNumber].GetBalance())
            {
                Console.WriteLine("\nTransaction ammount must be less than your current balance.\n");
                return false;
            }

            return true;
        }

        /// <summary>
        /// check to see if the number is positive
        /// </summary>
        /// <param name="transactionAmmount"></param>
        /// <returns>Bool</returns>
        private bool NegativeValidation(decimal transactionAmmount)
        {
            if(transactionAmmount <= 0)
            {
                Console.WriteLine("\nTransaction ammount must be greater than 0.\n");
                return false;
            }

            return true;
        }

        /// <summary>
        /// prompts the user for the correct ammount for this transaction
        /// </summary>
        /// <returns></returns>
        private decimal GetAmmount()
        {
            decimal transactionAmmount = 0;

            Console.WriteLine("\nPlease enter the ammount of this transaction.\n");
            string str = Console.ReadLine();

            try
            {
                return decimal.Parse(str);
            }
            catch
            {
                Console.WriteLine("\nUnable to process your selection, please try again...");
            }

            return transactionAmmount;
        }

        /// <summary>
        /// this method will get the last five trasactions of a customer but,
        /// it can display how ever many you might need
        /// </summary>
        /// <param name="numberOfTransactions">Int</param>
        protected override void GetLastTransactions()
        {
            int numberOfTransactions = 5;

            // now if this is a valid account number then we get the correct account
            // in a human readable format
            Console.WriteLine("\nThese are the last " + numberOfTransactions
                + " transactions for account number: " + this.accountNumber + "\n"
                + this.pseudoDatabase.GetData()[accountNumber].PrintLastTransactions(numberOfTransactions));
        }

        /// <summary>
        /// this will get the current balance of an account
        /// </summary>
        protected override void GetCurrentBalance()
        {
            // now if this is a valid account number then we get the correct account
            // in a human readable format
            Console.WriteLine("\nAccount balance for account number: " + this.accountNumber + "\n"
                + this.pseudoDatabase.GetData()[accountNumber].PrintBalance());
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
                + this.pseudoDatabase.GetData()[accountNumber].PrintBalance(date));
        }
    }
}
