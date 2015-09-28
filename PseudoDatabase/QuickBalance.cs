using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoDatabase
{
    /// <summary>
    /// this is used to make a quick list of info and make it sortable
    /// </summary>
    internal class QuickBalance
    {
        private decimal balance;
        private string accountInfo;
        
        // standard getters and setters
        public decimal GetBalance() {return this.balance;}
        public string GetAccountInfo() {return accountInfo;}
        
        public QuickBalance(decimal balance, string accountInfo)
        {
            this.balance = balance;
            this.accountInfo = accountInfo;
        }
    }
}
