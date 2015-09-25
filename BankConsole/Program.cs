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
            // this will run the program indefintely until the user prompts the exit sequence
            while (Greeter.RunProgram()) { }

            Console.WriteLine("\n\nGood-Bye...");
            Console.ReadKey();
        }
    }
}
