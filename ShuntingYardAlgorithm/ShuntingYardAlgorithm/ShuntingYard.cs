using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuntingYardAlgorithm
{
    class ShuntingYard
    {
        public string[] ConvertInfixToPostfix(string infix)
        {
            Queue<string> queue = new Queue<string>();
            Stack<string> stack = new Stack<string>();
            string[] infixArray = infix.Split(' ');
            
            foreach (string elem in infixArray)
            {
                switch (elem)
                {
                    case "^":
                        stack.Push(elem);
                        break;
                    case "*":
                        if (stack.Count > 0)
                        {
                            string op = stack.Peek();
                            if (op == "^" || op == "/")
                                queue.Enqueue(stack.Pop());
                        }
                        stack.Push(elem);
                        break;
                    case "/":
                        if (stack.Count > 0)
                        {
                            string op = stack.Peek();
                            if (op == "^" || op == "*")
                                queue.Enqueue(stack.Pop());
                        }
                        stack.Push(elem);
                        break;
                    case "+":
                        if (stack.Count > 0)
                        {
                            string op = stack.Peek();
                            if (op == "^" || op == "*" || op == "/" || op == "-")
                                queue.Enqueue(stack.Pop());
                        }
                        stack.Push(elem);
                        break;
                    case "-":
                        if (stack.Count > 0)
                        {
                            string op = stack.Peek();
                            if (op == "^" || op == "*" || op == "/" || op == "+")
                                queue.Enqueue(stack.Pop());
                        }
                        stack.Push(elem);
                        break;
                    case "(":
                        stack.Push(elem);
                        break;
                    case ")":
                        while (stack.Peek() != "(")
                            queue.Enqueue(stack.Pop());
                        stack.Pop();
                        break;
                    default:
                        queue.Enqueue(elem);
                        break;
                }
            }
            int count1 = stack.Count;
            for (int i = 0; i < count1; i++)
                queue.Enqueue(stack.Pop());

            
            int count2 = queue.Count;
            string[] postfix = new string[count2];
            for (int j = 0; j < count2; j++)
                postfix[j] = queue.Dequeue();

            return postfix;
        }

        public decimal CalculatePostfix(string[] postfix)
        {
            Stack<decimal> stack = new Stack<decimal>();
            decimal number = decimal.Zero;

            foreach (string elem in postfix)
            {
                if (decimal.TryParse(elem, out number))
                    stack.Push(number);
                else
                {
                    switch (elem)
                    {
                        case "^":
                            number = stack.Pop();
                            stack.Push(Exponent(stack.Pop(), number));
                            break;
                        case "*":
                            stack.Push(Multiplication(stack.Pop(), stack.Pop()));
                            break;
                        case "/":
                            number = stack.Pop();
                            stack.Push(Division(stack.Pop(), number));
                            break;
                        case "+":
                            stack.Push(Addition(stack.Pop(), stack.Pop()));
                            break;
                        case "-":
                            number = stack.Pop();
                            stack.Push(Subtraction(stack.Pop(), number));
                            break;
                        default:
                            Console.WriteLine("I have an error");
                            break;
                    }
                }
            }
            return stack.Pop();
        }

        private decimal Addition(decimal num1, decimal num2)
        {
            return num1 + num2;
        }

        private decimal Subtraction(decimal num1, decimal num2)
        {
            return num1 - num2;
        }

        private decimal Multiplication(decimal num1, decimal num2)
        {
            return num1 * num2;
        }

        private decimal Division(decimal num1, decimal num2)
        {
            return num1 / num2;
        }

        private decimal Exponent(decimal num1, decimal num2)
        {
            if(num2 == 0)
                return 1;
            else if (num2 == 1)
                return num1;
            else
            {
                decimal num = num1;
                for (int i = 2; i <= num2; i++)
                    num = num * num1;

                return num;
            }
        }
    }
}
