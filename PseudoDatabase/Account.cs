using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoDatabase
{
    public class Account
    {
        private string userName;
        private decimal balance;
        private Currency currency;
        private SortedDictionary<DateTime, Trasaction> transactionHistory;

        public Account(string userName, Currency currency, SortedDictionary<DateTime, Trasaction> transactionHistory)
        {
            this.userName = userName;
            this.currency = currency;
            this.transactionHistory = transactionHistory;
            this.balance = transactionHistory.Last().Value.GetBalance();
        }

        /// <summary>
        /// this is used for any time people want the current balance
        /// </summary>
        /// <returns>decimal balance</returns>
        public decimal GetBalance() { return balance; }
        public string GetUserName(){return userName;}
        public Currency GetCurrency(){return currency;}
        public SortedDictionary<DateTime, Trasaction> GetTransactionHistory() {return transactionHistory;}

        /// <summary>
        /// this is used for any time people want the current balance
        /// </summary>
        /// <returns>String balance</returns>
        public String PrintBalance() 
        { 
            return "\n  Customer:\t" + GetUserName()
                + "\n  Balance:\t" + Money.PrintCurrency(Currency.USD, this.balance)
                + "\n\n"; 
        }

        /// <summary>
        /// this is used for any time people want the current balance, given a currency
        /// </summary>
        /// <param name="currency"></param>
        /// <returns>String</returns>
        public String PrintBalance(Currency currency)
        {
            return "\n  Customer:\t" + GetUserName()
                + "\n  Balance:\t" + Money.PrintCurrency(currency, this.balance)
                + "\n\n";
        }

        /// <summary>
        /// this is used for any time people want the balance at a given time
        /// </summary>
        /// <param name="date">Date</param>
        /// <returns>String</returns>
        public String PrintBalance(DateTime date)
        {
            return "\n  Customer:\t" + GetUserName()
                + "\n  Balance:\t" + Money.PrintCurrency(Currency.USD, GetBalance(date))
                + "\n  Date:   \t" + date.ToShortDateString()
                + "\n\n";
        }

        /// <summary>
        /// gets the user balance for a certain date
        /// </summary>
        /// <param name="date">Date</param>
        /// <returns>decimal</returns>
        private decimal GetBalance(DateTime date)
        {
            // this will be returned
            decimal userBalance = this.balance;
            int count = transactionHistory.Count;

            // iterate through the transaction history untill you find the correct balance
            for(int i = (count - 1); i >= 0; i--)
            {
                userBalance = transactionHistory.ElementAt(i).Value.GetBalance();

                if (DateTime.Compare(transactionHistory.ElementAt(i).Key, date) < 0)
                {
                    return userBalance;
                }
            }

            return userBalance;
        }

        public string PrintLastTransactions(int numberOfTransactions)
        {
            // just some string formatting
            StringBuilder text = new StringBuilder();
            text.Append(GetUserName());
            text.Append("'s last ");
            text.Append(numberOfTransactions.ToString());
            text.AppendLine(" transactions are as follows...");

            int count = transactionHistory.Count;

            // iterate through the transaction for the given number of times
            for (int i = (count - 1); i >= 0; i--)
            {
                text.Append("\n  Date:   \t").AppendLine(transactionHistory.ElementAt(i).Key.ToShortDateString());
                text.AppendLine(transactionHistory.ElementAt(i).Value.PrintTransaction());
                numberOfTransactions--;

                if (numberOfTransactions <= 0)
                {
                    return text.ToString();
                }
            }

            return text.ToString();
        }
    }
}
