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

    public enum Currency
    {
        Dollar,
        Euro
    }

    public class Account
    {
        private string userName;
        private decimal balance;
        private Currency currency;
        private SortedDictionary<DateTime, Trasaction> transactionHistory;

        public Account(string userName, decimal balance, Currency currency, SortedDictionary<DateTime, Trasaction> transactionHistory)
        {
            this.userName = userName;
            this.balance = balance;
            this.currency = currency;
            this.transactionHistory = transactionHistory;
        }

        public string GetUserName(){return userName;}
        public decimal GetBalance(){return balance;}
        public Currency GetCurrency(){return currency;}
        public SortedDictionary<DateTime, Trasaction> GetTransactionHistory() {return transactionHistory;}
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

        public decimal GetBalance(){ return balance; }
        public decimal GetAmmount(){ return ammount; }
        public TransactionType GetTransactionType(){ return transactionType; }
        public Currency GetCurrency(){ return currency; }
        
        // only the database constructor should use this
        internal void SetBalance(decimal newBalance) 
        {
            balance = newBalance;
        }
    }
}
