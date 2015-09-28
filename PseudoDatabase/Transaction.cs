using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoDatabase
{
    public enum TransactionType
    {
        Withdrawl,
        Deposit,
        RecieveFromAccount,
        SendToAccount
    }

    public class Trasaction
    {
        private decimal balance;
        private decimal ammount;
        private TransactionType transactionType;
        private Currency currency;

        public Trasaction(decimal balance, decimal ammount, TransactionType transactionType, Currency currency)
        {
            this.balance = balance;
            this.ammount = ammount;
            this.transactionType = transactionType;
            this.currency = currency;
        }

        public decimal GetBalance() { return balance; }
        public decimal GetAmmount() { return ammount; }
        public TransactionType GetTransactionType() { return transactionType; }
        public Currency GetCurrency() { return currency; }

        // only the database constructor should use this
        internal void SetBalance(decimal newBalance)
        {
            this.balance = newBalance;
        }

        // this is used for a nice looking output
        public string PrintTransaction()
        {
            string str = "-";

            if (GetTransactionType() == TransactionType.Deposit || GetTransactionType() == TransactionType.RecieveFromAccount)
            {
                str = "+";
            }

            return "  Balance:\t" + Money.PrintCurrency(Currency.Dollar,this.balance)
                + "\n  Ammount:\t" + str + Money.PrintCurrency(Currency.Dollar, this.ammount)
                + "\n  Type:   \t" + GetTransactionType().ToString();
        }       
    }
}
