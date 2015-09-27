using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoDatabase
{
    /// <summary>
    /// this holds all the logic for the database which is a Dictionary.
    /// Key: Account #,
    /// Value: Account
    /// </summary>
    public class Database
    {
        private Dictionary<int, Account> accountDatabase;

        public Database()
        {
            this.accountDatabase = createDatabase();
        }

        /// <summary>
        /// this is just to populate the database, its an ugly but quick solution
        /// </summary>
        /// <returns></returns>
        private Dictionary<int, Account> createDatabase()
        {
            Random random = new Random();
            Dictionary<int, Account> accountDatabase = new Dictionary<int, Account>();
            SortedDictionary<DateTime, Trasaction> transactionHistory = new SortedDictionary<DateTime, Trasaction>();

            // this was just to help set things up quickly
            int count = 10000;
            string[] customers = { "Candelaria Lavigne","Particia Willman","Daniella Yager","Penelope Corriveau","Rodney Mckoy","Nanette Gangi",
                "Ernestine Brunet","Francesca Letsinger","Jospeh Murr","Meagan Merrill","Carie Reifsteck","Charline Acedo","Elaine Oyer",
                "Kristi Kime","Lashaun Kindel","Iraida Mcpeak","Dagmar Gaskill","Lieselotte Ingrassia","Pennie Folts","Enid Stairs","Tawana Tapia",
                "Huey Hollon","Windy Grindle","Manuela Langlinais","Susana Penna","Jazmine Bruso","Hubert Nivens","Chrissy Shelly","Greta Deshotel",
                "Anibal Yoshimura","Buck Cleaver","Nida Rossetti","Elliot Rosso","Lovetta Blazier","Robbin Ketron","Bennett Gassett","Bella Hunsicker",
                "Laurel Marchese","Danyell Parenti","Bulah Livermore","Tashina Parkhill","Rossie Croston","Marissa Primus","Ed Pundt","Richelle Licht",
                "Traci Murchison","Elise Amato","Jacquelyn Kaya","Elida Pickney","Joaquina Iacovelli" };

            foreach (string customer in customers)
            {
                count++;
                transactionHistory = getHistory();
                accountDatabase.Add(count, new Account(customer, (decimal)1.22, Currency.Dollar, transactionHistory));
            }

            return accountDatabase;
        }

        /// <summary>
        /// create a fake history for each customer
        /// </summary>
        /// <returns></returns>
        private SortedDictionary<DateTime, Trasaction> getHistory()
        {
            SortedDictionary<DateTime, Trasaction> transactionHistory = new SortedDictionary<DateTime, Trasaction>();
            Random random = new Random();
            decimal startingAmmount = random.Next(1000,10000);

            while(transactionHistory.Count < 10)
            {
                int year = random.Next(1980, 2015);
                int month = random.Next(1, 12);
                int day = random.Next(1, 28);
                DateTime date = new DateTime(year,month,day);
                int transType = random.Next(1, 4);
                int ammount = random.Next(1, 1000);

                if(!transactionHistory.ContainsKey(date))
                {
                    transactionHistory.Add(date, new Trasaction(startingAmmount, ammount, (TransactionType)transType, Currency.Dollar));
                }
            }

            for(int i = 0; i < 10; i++)
            {
                if(i > 0)
                {
                    decimal oldBalance = transactionHistory.ElementAt(i - 1).Value.GetBalance();
                    decimal difference = transactionHistory.ElementAt(i).Value.GetAmmount();

                    if(transactionHistory.ElementAt(i).Value.GetTransactionType() == TransactionType.Deposit ||
                        transactionHistory.ElementAt(i).Value.GetTransactionType() == TransactionType.RecieveFromAccount)
                    {
                        transactionHistory.ElementAt(i).Value.SetBalance(oldBalance + difference);
                    }
                    else
                    {
                        transactionHistory.ElementAt(i).Value.SetBalance(oldBalance - difference);
                    }
                }
            }

            return transactionHistory;
        }
    }
}
