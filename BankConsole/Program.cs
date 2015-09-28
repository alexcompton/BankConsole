using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // this will allow the greeter to have a local version of the constructed database
            // this would really be in a database for now its stored locally
            Greeter greeter = new Greeter();

            // this will run the program indefintely until the user prompts the exit sequence
            while (greeter.RunProgram()) { }

            Console.WriteLine("\n\nGood-Bye...");
            Console.ReadKey();
        }
    }
}
