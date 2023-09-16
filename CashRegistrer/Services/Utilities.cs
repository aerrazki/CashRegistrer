using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegistrer.Services
{
    public class Utilities
    {
        public static int ReadIntegerFromConsole(string prompt)
        {
            int number;
            bool isValidInput = false;

            do
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (int.TryParse(input, out number))
                {
                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer.");
                }

            } while (!isValidInput);

            return number;
        }

        public static string ReadStringFromConsole(string prompt)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            return input;
        }

    }
}
