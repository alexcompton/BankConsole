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
                "\n  7) View your current balance in a foriegn currency." +
                "\n  8) Exit.\n");
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
                        GetConvertedBalance();
                        return true;
                    case 8:
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
                transactionHistory.Add(DateTime.Now, new Trasaction(newBalance, transactionAmmount, TransactionType.Deposit, Currency.USD));
                this.pseudoDatabase.GetData()[accountNumber] = new Account(this.pseudoDatabase.GetData()[accountNumber].GetUserName(), Currency.USD, transactionHistory);

                // now do the same for the other account
                // set things up so we can override in data base
                transactionHistory = this.pseudoDatabase.GetData()[recivingAccountNumber].GetTransactionHistory();
                newBalance = this.pseudoDatabase.GetData()[recivingAccountNumber].GetBalance() + transactionAmmount;
                // here we save the transaction
                transactionHistory.Add(DateTime.Now, new Trasaction(newBalance, transactionAmmount, TransactionType.Deposit, Currency.USD));
                this.pseudoDatabase.GetData()[recivingAccountNumber] = new Account(this.pseudoDatabase.GetData()[recivingAccountNumber].GetUserName(), Currency.USD, transactionHistory);
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
                transactionHistory.Add(DateTime.Now, new Trasaction(newBalance,transactionAmmount,TransactionType.Deposit,Currency.USD));
                this.pseudoDatabase.GetData()[accountNumber] = new Account(this.pseudoDatabase.GetData()[accountNumber].GetUserName(), Currency.USD, transactionHistory);
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
                transactionHistory.Add(DateTime.Now, new Trasaction(newBalance, transactionAmmount, TransactionType.Deposit, Currency.USD));
                this.pseudoDatabase.GetData()[accountNumber] = new Account(this.pseudoDatabase.GetData()[accountNumber].GetUserName(), Currency.USD, transactionHistory);
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

        /// <summary>
        /// I know this one is long it has a lot of options
        /// </summary>
        private void GetConvertedBalance()
        {
            Console.WriteLine("\nPlease select the three letter currency code you wish to use.\n(Hint: AED would select the UAE Dirham)\n"
                    + "\n  AED: United Arab Emirates Dirham"
                    + "\n  AFN: Afghan Afghani"
                    + "\n  ALL: Albanian Lek"
                    + "\n  AMD: Armenian Dram"
                    + "\n  ANG: Netherlands Antillean Guilder"
                    + "\n  AOA: Angolan Kwanza"
                    + "\n  ARS: Argentine Peso"
                    + "\n  AUD: Australian Dollar"
                    + "\n  AWG: Aruban Florin"
                    + "\n  AZN: Azerbaijani Manat"
                    + "\n  BAM: Bosnia-Herzegovina Convertible Mark"
                    + "\n  BBD: Barbadian Dollar"
                    + "\n  BDT: Bangladeshi Taka"
                    + "\n  BGN: Bulgarian Lev"
                    + "\n  BHD: Bahraini Dinar"
                    + "\n  BIF: Burundian Franc"
                    + "\n  BMD: Bermudan Dollar"
                    + "\n  BND: Brunei Dollar"
                    + "\n  BOB: Bolivian Boliviano"
                    + "\n  BRL: Brazilian Real"
                    + "\n  BSD: Bahamian Dollar"
                    + "\n  BTC: Bitcoin"
                    + "\n  BTN: Bhutanese Ngultrum"
                    + "\n  BWP: Botswanan Pula"
                    + "\n  BYR: Belarusian Ruble"
                    + "\n  BZD: Belize Dollar"
                    + "\n  CAD: Canadian Dollar"
                    + "\n  CDF: Congolese Franc"
                    + "\n  CHF: Swiss Franc"
                    + "\n  CLF: Chilean Unit of Account (UF)"
                    + "\n  CLP: Chilean Peso"
                    + "\n  CNY: Chinese Yuan"
                    + "\n  COP: Colombian Peso"
                    + "\n  CRC: Costa Rican Colón"
                    + "\n  CUC: Cuban Convertible Peso"
                    + "\n  CUP: Cuban Peso"
                    + "\n  CVE: Cape Verdean Escudo"
                    + "\n  CZK: Czech Republic Koruna"
                    + "\n  DJF: Djiboutian Franc"
                    + "\n  DKK: Danish Krone"
                    + "\n  DOP: Dominican Peso"
                    + "\n  DZD: Algerian Dinar"
                    + "\n  EEK: Estonian Kroon"
                    + "\n  EGP: Egyptian Pound"
                    + "\n  ERN: Eritrean Nakfa"
                    + "\n  ETB: Ethiopian Birr"
                    + "\n  EUR: Euro"
                    + "\n  FJD: Fijian Dollar"
                    + "\n  FKP: Falkland Islands Pound"
                    + "\n  GBP: British Pound Sterling"
                    + "\n  GEL: Georgian Lari"
                    + "\n  GGP: Guernsey Pound"
                    + "\n  GHS: Ghanaian Cedi"
                    + "\n  GIP: Gibraltar Pound"
                    + "\n  GMD: Gambian Dalasi"
                    + "\n  GNF: Guinean Franc"
                    + "\n  GTQ: Guatemalan Quetzal"
                    + "\n  GYD: Guyanaese Dollar"
                    + "\n  HKD: Hong Kong Dollar"
                    + "\n  HNL: Honduran Lempira"
                    + "\n  HRK: Croatian Kuna"
                    + "\n  HTG: Haitian Gourde"
                    + "\n  HUF: Hungarian Forint"
                    + "\n  IDR: Indonesian Rupiah"
                    + "\n  ILS: Israeli New Sheqel"
                    + "\n  IMP: Manx pound"
                    + "\n  INR: Indian Rupee"
                    + "\n  IQD: Iraqi Dinar"
                    + "\n  IRR: Iranian Rial"
                    + "\n  ISK: Icelandic Króna"
                    + "\n  JEP: Jersey Pound"
                    + "\n  JMD: Jamaican Dollar"
                    + "\n  JOD: Jordanian Dinar"
                    + "\n  JPY: Japanese Yen"
                    + "\n  KES: Kenyan Shilling"
                    + "\n  KGS: Kyrgystani Som"
                    + "\n  KHR: Cambodian Riel"
                    + "\n  KMF: Comorian Franc"
                    + "\n  KPW: North Korean Won"
                    + "\n  KRW: South Korean Won"
                    + "\n  KWD: Kuwaiti Dinar"
                    + "\n  KYD: Cayman Islands Dollar"
                    + "\n  KZT: Kazakhstani Tenge"
                    + "\n  LAK: Laotian Kip"
                    + "\n  LBP: Lebanese Pound"
                    + "\n  LKR: Sri Lankan Rupee"
                    + "\n  LRD: Liberian Dollar"
                    + "\n  LSL: Lesotho Loti"
                    + "\n  LTL: Lithuanian Litas"
                    + "\n  LVL: Latvian Lats"
                    + "\n  LYD: Libyan Dinar"
                    + "\n  MAD: Moroccan Dirham"
                    + "\n  MDL: Moldovan Leu"
                    + "\n  MGA: Malagasy Ariary"
                    + "\n  MKD: Macedonian Denar"
                    + "\n  MMK: Myanma Kyat"
                    + "\n  MNT: Mongolian Tugrik"
                    + "\n  MOP: Macanese Pataca"
                    + "\n  MRO: Mauritanian Ouguiya"
                    + "\n  MTL: Maltese Lira"
                    + "\n  MUR: Mauritian Rupee"
                    + "\n  MVR: Maldivian Rufiyaa"
                    + "\n  MWK: Malawian Kwacha"
                    + "\n  MXN: Mexican Peso"
                    + "\n  MYR: Malaysian Ringgit"
                    + "\n  MZN: Mozambican Metical"
                    + "\n  NAD: Namibian Dollar"
                    + "\n  NGN: Nigerian Naira"
                    + "\n  NIO: Nicaraguan Córdoba"
                    + "\n  NOK: Norwegian Krone"
                    + "\n  NPR: Nepalese Rupee"
                    + "\n  NZD: New Zealand Dollar"
                    + "\n  OMR: Omani Rial"
                    + "\n  PAB: Panamanian Balboa"
                    + "\n  PEN: Peruvian Nuevo Sol"
                    + "\n  PGK: Papua New Guinean Kina"
                    + "\n  PHP: Philippine Peso"
                    + "\n  PKR: Pakistani Rupee"
                    + "\n  PLN: Polish Zloty"
                    + "\n  PYG: Paraguayan Guarani"
                    + "\n  QAR: Qatari Rial"
                    + "\n  RON: Romanian Leu"
                    + "\n  RSD: Serbian Dinar"
                    + "\n  RUB: Russian Ruble"
                    + "\n  RWF: Rwandan Franc"
                    + "\n  SAR: Saudi Riyal"
                    + "\n  SBD: Solomon Islands Dollar"
                    + "\n  SCR: Seychellois Rupee"
                    + "\n  SDG: Sudanese Pound"
                    + "\n  SEK: Swedish Krona"
                    + "\n  SGD: Singapore Dollar"
                    + "\n  SHP: Saint Helena Pound"
                    + "\n  SLL: Sierra Leonean Leone"
                    + "\n  SOS: Somali Shilling"
                    + "\n  SRD: Surinamese Dollar"
                    + "\n  STD: São Tomé and Príncipe Dobra"
                    + "\n  SVC: Salvadoran Colón"
                    + "\n  SYP: Syrian Pound"
                    + "\n  SZL: Swazi Lilangeni"
                    + "\n  THB: Thai Baht"
                    + "\n  TJS: Tajikistani Somoni"
                    + "\n  TMT: Turkmenistani Manat"
                    + "\n  TND: Tunisian Dinar"
                    + "\n  TOP: Tongan Paʻanga"
                    + "\n  TRY: Turkish Lira"
                    + "\n  TTD: Trinidad and Tobago Dollar"
                    + "\n  TWD: New Taiwan Dollar"
                    + "\n  TZS: Tanzanian Shilling"
                    + "\n  UAH: Ukrainian Hryvnia"
                    + "\n  UGX: Ugandan Shilling"
                    + "\n  USD: United States Dollar"
                    + "\n  UYU: Uruguayan Peso"
                    + "\n  UZS: Uzbekistan Som"
                    + "\n  VEF: Venezuelan Bolívar Fuerte"
                    + "\n  VND: Vietnamese Dong"
                    + "\n  VUV: Vanuatu Vatu"
                    + "\n  WST: Samoan Tala"
                    + "\n  XAF: CFA Franc BEAC"
                    + "\n  XAG: Silver (troy ounce)"
                    + "\n  XAU: Gold (troy ounce)"
                    + "\n  XCD: East Caribbean Dollar"
                    + "\n  XDR: Special Drawing Rights"
                    + "\n  XOF: CFA Franc BCEAO"
                    + "\n  XPD: Palladium Ounce"
                    + "\n  XPF: CFP Franc"
                    + "\n  XPT: Platinum Ounce"
                    + "\n  YER: Yemeni Rial"
                    + "\n  ZAR: South African Rand"
                    + "\n  ZMK: Zambian Kwacha (pre-2013)"
                    + "\n  ZMW: Zambian Kwacha"
                    + "\n  ZWL: Zimbabwean Dollar\n");

            // get the user input
            String str = Console.ReadLine();

            try
            {
                Currency currency = (Currency)Enum.Parse(typeof(Currency), str.ToUpper(), true);

                // now if this is a valid account number then we get the correct account
                // in a human readable format
                Console.WriteLine("\nAccount balance for account number: " + this.accountNumber + "\n"
                    + this.pseudoDatabase.GetData()[accountNumber].PrintBalance(currency));
            }
            catch
            {
                Console.WriteLine("\nUnable to process your request. Please try again...");
            }
        }
    }
}
