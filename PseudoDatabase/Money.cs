using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PseudoDatabase
{
    /// <summary>
    /// this houses the supported currency types its an enum so we can easily add more if needed
    /// </summary>
    public enum Currency
    {
        AED,
        AFN,
        ALL,
        AMD,
        ANG,
        AOA,
        ARS,
        AUD,
        AWG,
        AZN,
        BAM,
        BBD,
        BDT,
        BGN,
        BHD,
        BIF,
        BMD,
        BND,
        BOB,
        BRL,
        BSD,
        BTC,
        BTN,
        BWP,
        BYR,
        BZD,
        CAD,
        CDF,
        CHF,
        CLF,
        CLP,
        CNY,
        COP,
        CRC,
        CUC,
        CUP,
        CVE,
        CZK,
        DJF,
        DKK,
        DOP,
        DZD,
        EEK,
        EGP,
        ERN,
        ETB,
        EUR,
        FJD,
        FKP,
        GBP,
        GEL,
        GGP,
        GHS,
        GIP,
        GMD,
        GNF,
        GTQ,
        GYD,
        HKD,
        HNL,
        HRK,
        HTG,
        HUF,
        IDR,
        ILS,
        IMP,
        INR,
        IQD,
        IRR,
        ISK,
        JEP,
        JMD,
        JOD,
        JPY,
        KES,
        KGS,
        KHR,
        KMF,
        KPW,
        KRW,
        KWD,
        KYD,
        KZT,
        LAK,
        LBP,
        LKR,
        LRD,
        LSL,
        LTL,
        LVL,
        LYD,
        MAD,
        MDL,
        MGA,
        MKD,
        MMK,
        MNT,
        MOP,
        MRO,
        MTL,
        MUR,
        MVR,
        MWK,
        MXN,
        MYR,
        MZN,
        NAD,
        NGN,
        NIO,
        NOK,
        NPR,
        NZD,
        OMR,
        PAB,
        PEN,
        PGK,
        PHP,
        PKR,
        PLN,
        PYG,
        QAR,
        RON,
        RSD,
        RUB,
        RWF,
        SAR,
        SBD,
        SCR,
        SDG,
        SEK,
        SGD,
        SHP,
        SLL,
        SOS,
        SRD,
        STD,
        SVC,
        SYP,
        SZL,
        THB,
        TJS,
        TMT,
        TND,
        TOP,
        TRY,
        TTD,
        TWD,
        TZS,
        UAH,
        UGX,
        USD,
        UYU,
        UZS,
        VEF,
        VND,
        VUV,
        WST,
        XAF,
        XAG,
        XAU,
        XCD,
        XDR,
        XOF,
        XPD,
        XPF,
        XPT,
        YER,
        ZAR,
        ZMK,
        ZMW,
        ZWL
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

            if (currency.Equals(Currency.USD))
            {
                return String.Format("{0:C}", dollarAmmount);
            }
            else
            {
                decimal exchangeRate = GetTransferRate(Enum.GetName(typeof(Currency), currency));
                return String.Format("{0:f2} {1}", (dollarAmmount * exchangeRate), Enum.GetName(typeof(Currency), currency));
            }
        }

        private static decimal GetTransferRate(string currencyCode)
        {
            // The downloaded resource ends up in the variable named content.
            var content = new MemoryStream();

            // Initialize an HttpWebRequest for the current URL.
            var webReq = (HttpWebRequest)WebRequest.Create("https://openexchangerates.org/api/latest.json?app_id=ef8958b334634409a923dca794877f4f");

            // Send the request to the Internet resource and wait for
            // the response.
            using (WebResponse response = webReq.GetResponse())
            {
                // Get the data stream that is associated with the specified url.
                using (Stream responseStream = response.GetResponseStream())
                {
                    responseStream.CopyTo(content);
                }
            }

            // put it in a string to be serialized
            content.Position = 0;
            var sr = new StreamReader(content);
            var jsonStr = sr.ReadToEnd();

            // i had to create a few thing just to serialize
            ExchangeRate exr = JsonConvert.DeserializeObject<ExchangeRate>(jsonStr);
            Rates rates = exr.rates;

            // this reflection is ugly and can probably be reworked
            var dec = rates.GetType().GetProperty(currencyCode).GetValue(rates, null);

            // Return the result as a byte array.
            return (decimal)dec;
        }
    }
        
     
    /// <summary>
    /// I had to create these classes because the seralizer needed it to work
    /// </summary>
    internal class ExchangeRate
    {
        public string disclaimer { get; set; }
        public string license { get; set; }
        public int timestamp { get; set; }
        public Rates rates { get; set; }
    }

    internal class Rates
    {
        public decimal AED { get; set; }
        public decimal AFN { get; set; }
        public decimal ALL { get; set; }
        public decimal AMD { get; set; }
        public decimal ANG { get; set; }
        public decimal AOA { get; set; }
        public decimal ARS { get; set; }
        public decimal AUD { get; set; }
        public decimal AWG { get; set; }
        public decimal AZN { get; set; }
        public decimal BAM { get; set; }
        public decimal BBD { get; set; }
        public decimal BDT { get; set; }
        public decimal BGN { get; set; }
        public decimal BHD { get; set; }
        public decimal BIF { get; set; }
        public decimal BMD { get; set; }
        public decimal BND { get; set; }
        public decimal BOB { get; set; }
        public decimal BRL { get; set; }
        public decimal BSD { get; set; }
        public decimal BTC { get; set; }
        public decimal BTN { get; set; }
        public decimal BWP { get; set; }
        public decimal BYR { get; set; }
        public decimal BZD { get; set; }
        public decimal CAD { get; set; }
        public decimal CDF { get; set; }
        public decimal CHF { get; set; }
        public decimal CLF { get; set; }
        public decimal CLP { get; set; }
        public decimal CNY { get; set; }
        public decimal COP { get; set; }
        public decimal CRC { get; set; }
        public decimal CUC { get; set; }
        public decimal CUP { get; set; }
        public decimal CVE { get; set; }
        public decimal CZK { get; set; }
        public decimal DJF { get; set; }
        public decimal DKK { get; set; }
        public decimal DOP { get; set; }
        public decimal DZD { get; set; }
        public decimal EEK { get; set; }
        public decimal EGP { get; set; }
        public decimal ERN { get; set; }
        public decimal ETB { get; set; }
        public decimal EUR { get; set; }
        public decimal FJD { get; set; }
        public decimal FKP { get; set; }
        public decimal GBP { get; set; }
        public decimal GEL { get; set; }
        public decimal GGP { get; set; }
        public decimal GHS { get; set; }
        public decimal GIP { get; set; }
        public decimal GMD { get; set; }
        public decimal GNF { get; set; }
        public decimal GTQ { get; set; }
        public decimal GYD { get; set; }
        public decimal HKD { get; set; }
        public decimal HNL { get; set; }
        public decimal HRK { get; set; }
        public decimal HTG { get; set; }
        public decimal HUF { get; set; }
        public decimal IDR { get; set; }
        public decimal ILS { get; set; }
        public decimal IMP { get; set; }
        public decimal INR { get; set; }
        public decimal IQD { get; set; }
        public decimal IRR { get; set; }
        public decimal ISK { get; set; }
        public decimal JEP { get; set; }
        public decimal JMD { get; set; }
        public decimal JOD { get; set; }
        public decimal JPY { get; set; }
        public decimal KES { get; set; }
        public decimal KGS { get; set; }
        public decimal KHR { get; set; }
        public decimal KMF { get; set; }
        public decimal KPW { get; set; }
        public decimal KRW { get; set; }
        public decimal KWD { get; set; }
        public decimal KYD { get; set; }
        public decimal KZT { get; set; }
        public decimal LAK { get; set; }
        public decimal LBP { get; set; }
        public decimal LKR { get; set; }
        public decimal LRD { get; set; }
        public decimal LSL { get; set; }
        public decimal LTL { get; set; }
        public decimal LVL { get; set; }
        public decimal LYD { get; set; }
        public decimal MAD { get; set; }
        public decimal MDL { get; set; }
        public decimal MGA { get; set; }
        public decimal MKD { get; set; }
        public decimal MMK { get; set; }
        public decimal MNT { get; set; }
        public decimal MOP { get; set; }
        public decimal MRO { get; set; }
        public decimal MTL { get; set; }
        public decimal MUR { get; set; }
        public decimal MVR { get; set; }
        public decimal MWK { get; set; }
        public decimal MXN { get; set; }
        public decimal MYR { get; set; }
        public decimal MZN { get; set; }
        public decimal NAD { get; set; }
        public decimal NGN { get; set; }
        public decimal NIO { get; set; }
        public decimal NOK { get; set; }
        public decimal NPR { get; set; }
        public decimal NZD { get; set; }
        public decimal OMR { get; set; }
        public decimal PAB { get; set; }
        public decimal PEN { get; set; }
        public decimal PGK { get; set; }
        public decimal PHP { get; set; }
        public decimal PKR { get; set; }
        public decimal PLN { get; set; }
        public decimal PYG { get; set; }
        public decimal QAR { get; set; }
        public decimal RON { get; set; }
        public decimal RSD { get; set; }
        public decimal RUB { get; set; }
        public decimal RWF { get; set; }
        public decimal SAR { get; set; }
        public decimal SBD { get; set; }
        public decimal SCR { get; set; }
        public decimal SDG { get; set; }
        public decimal SEK { get; set; }
        public decimal SGD { get; set; }
        public decimal SHP { get; set; }
        public decimal SLL { get; set; }
        public decimal SOS { get; set; }
        public decimal SRD { get; set; }
        public decimal STD { get; set; }
        public decimal SVC { get; set; }
        public decimal SYP { get; set; }
        public decimal SZL { get; set; }
        public decimal THB { get; set; }
        public decimal TJS { get; set; }
        public decimal TMT { get; set; }
        public decimal TND { get; set; }
        public decimal TOP { get; set; }
        public decimal TRY { get; set; }
        public decimal TTD { get; set; }
        public decimal TWD { get; set; }
        public decimal TZS { get; set; }
        public decimal UAH { get; set; }
        public decimal UGX { get; set; }
        public decimal USD { get; set; }
        public decimal UYU { get; set; }
        public decimal UZS { get; set; }
        public decimal VEF { get; set; }
        public decimal VND { get; set; }
        public decimal VUV { get; set; }
        public decimal WST { get; set; }
        public decimal XAF { get; set; }
        public decimal XAG { get; set; }
        public decimal XAU { get; set; }
        public decimal XCD { get; set; }
        public decimal XDR { get; set; }
        public decimal XOF { get; set; }
        public decimal XPD { get; set; }
        public decimal XPF { get; set; }
        public decimal XPT { get; set; }
        public decimal YER { get; set; }
        public decimal ZAR { get; set; }
        public decimal ZMK { get; set; }
        public decimal ZMW { get; set; }
        public decimal ZWL { get; set; }
    }
}
