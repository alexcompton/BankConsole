using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portals
{
    public abstract class User
    {
        /// <summary>
        /// this actually just runs the portal until the user decides to leave
        /// </summary>
        public void StartPortal()
        {
            Console.WriteLine("\n\nWelcome to the First Third " + this.GetType().Name + " Department Portal.\n");
            while (RunPortal()) { }
        }

        abstract protected bool RunPortal();
        abstract protected void GetBalanceAtDate();
        abstract protected void GetCurrentBalance();
        abstract protected void GetLastTransactions(int numberOfTransactions);

        /// <summary>
        /// gets the date from a user input
        /// </summary>
        /// <returns></returns>
        protected DateTime GetDateFromUser(int accountNumber)
        {
            // get the date from the user
            DateTime date = new DateTime(1980, 1, 1);
            Console.WriteLine("\nPlease enter a date in the following format MM/DD/YYYY."
                + "\nThis will retrieve the balance on that date for account number: " + accountNumber
                + "\n(Hint: Our bank was founded 1/1/1980)\n");
            string userInput = Console.ReadLine();

            try
            {
                string[] dateArray = userInput.Split('/');
                date = new DateTime(int.Parse(dateArray[2]), int.Parse(dateArray[0]), int.Parse(dateArray[1]));

                if (DateTime.Compare(date, new DateTime(1980, 1, 1)) < 0)
                {
                    date = new DateTime(1980, 1, 1);
                    Console.WriteLine("\nOur bank was founded in 1/1/1980.\nYour selection is invalid,  please try again...");
                }
            }
            catch
            {
                Console.WriteLine("\nCouldn't process your selection, please try again...");
            }

            return date;
        }
    }
}
