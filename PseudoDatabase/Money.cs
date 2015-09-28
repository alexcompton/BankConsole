using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoDatabase
{
    /// <summary>
    /// this houses the supported currency types its an enum so we can easily add more if needed
    /// </summary>
    public enum Currency
    {
        Dollar,
        Euro
    }

    class Money
    {
        /// <summary>
        /// this can be reused for whatever currency type you would like to add if you need more in the future
        /// </summary>
        /// <param name="currency"></param>
        /// <param name="userBalance"></param>
        /// <returns></returns>
        public static String PrintCurrency(Currency currency, decimal dollarAmmount)
        {
            switch (currency)
            {
                case Currency.Dollar:
                    return String.Format("{0:C}", dollarAmmount);
                case Currency.Euro:
                    return String.Format("{0:C}", dollarAmmount); ;
                default:
                    throw new Exception("This currency type isn't supported yet.");
            }
        }
    }
}
