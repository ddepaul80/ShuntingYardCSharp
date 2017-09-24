using System;
using System.Collections.Generic;
using System.Text;

namespace ShuntingYardAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] infix = { "3 + 4", "3 + 4 * 2 / ( 1 - 5 ) ^ 2 ^ 3", "2 * 3 - 4 / 5", "( 4 + 8 ) * ( 6 - 5 ) / ( ( 3 - 2 ) * ( 2 + 2 ) )", "( 300 + 23 ) * ( 43 - 21 ) / ( 84 + 7 )" };
            //string[] infix = { "5 + 2 ^ 3", "2 + 1 - 12 / 3", "162 / ( 2 + 1 ) ^ 4" };
            //string[] infix = {"4 ^ 2", "5 + 3 ^ 3" };
            ShuntingYard sy = new ShuntingYard();

            foreach (string elem in infix)
            {
                Console.WriteLine("Infix notation: " + elem);
                string[] postfix = sy.ConvertInfixToPostfix(elem);
                Console.Write("Postfix notation: ");
                foreach (string postfixElem in postfix)
                    Console.Write(postfixElem + " ");

                Console.WriteLine();

                decimal val = sy.CalculatePostfix(postfix);
                Console.WriteLine("Result: " + val);
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
