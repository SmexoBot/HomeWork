using Microsoft.Win32;
using System;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace Lists
{
    public class Tocen
    {

    }

    public class Operation : Tocen
    {
        public char Symbol;
        public int Priority;
    }

    public class Parenthesis : Tocen
    {
        public bool IsOpen;
    }

    public class Numberss : Tocen
    {
        public double Number;
    }
    public static class Program
    {
        public static List<Tocen> List(string inp)
        {
            inp = inp + ' ';
            inp = inp.Replace('.', ',');
            int len = inp.Length - 1;
            List<Tocen> list = new List<Tocen>();

            for (int i = 0; i < len; i++)
            {
                if (inp[i] == ' ')
                {
                    continue;
                }
                else if (char.IsDigit(inp[i]))
                {
                    string str = Numbers(i, 1, inp);
                    Numberss num = new Numberss();
                    num.Number = Convert.ToDouble(str);
                    list.Add(num);
                    i = i + str.Length - 1;
                }
                else
                {
                    if (inp[i] == '(')
                    {
                        Parenthesis parenthesis = new Parenthesis();
                        parenthesis.IsOpen = true;
                        list.Add(parenthesis);
                    }
                    else if (inp[i] == ')')
                    {
                        Parenthesis parenthesis = new Parenthesis();
                        parenthesis.IsOpen = false;
                        list.Add(parenthesis);
                    }
                    else
                    {
                        Operation op = new Operation();
                        if (inp[i] == '*')
                        {
                            op.Symbol = '*';
                            op.Priority = 1;
                        }
                        else if (inp[i] == '/')
                        {
                            op.Symbol = '/';
                            op.Priority = 1;
                        }
                        else if (inp[i] == '+')
                        {
                            op.Symbol = '+';
                        }
                        else
                        {
                            op.Symbol = '-';
                        }
                        list.Add(op);
                    }
                }
            }
            return list;
        }
        public static string Numbers(int i, int index, string inp)
        {
            while (true)
            {
                if (Char.IsDigit(inp[i + index]) || inp[i + index] == ',')
                {
                    index++;
                }
                else
                {
                    return inp.Substring(i, index);
                }
            }
        }

        public static List<Tocen> ToRpn(List<Tocen> inp)
        {
            List <Tocen> list = new List<Tocen>();
            Stack<char> op = new Stack<char>();
            op.Push(' ');
            int len = list.Count();

            for (int i = 0; i < len; i++)
            {

            }
            
        }

        public static void PrintList(List<object> list)
        {
            foreach (object o in list)
            {
                Console.Write(o);
                Console.Write(' ');
            }
            Console.WriteLine();
        }

        public static double Calculate(char op, double num1, double num2)
        {
            switch (op)
            {
                case '*': return num1 * num2;
                case '/': return num1 / num2;
                case '+': return num1 + num2;
                case '-': return num1 - num2;
            }
            Console.WriteLine("ERROR");
            return 0;
        }

        public static void Main()
        {
            string inp = Console.ReadLine();
            List<Tocen> expression = ToRpn(List(inp));
            PrintList(expression);
            char check = ' ';
            /*
            for (int i = 0; i < expression.Count; i++)
            {
                if (Object.ReferenceEquals(expression[i].GetType(), check.GetType()))
                {
                    double num1 = (double)expression[i - 2];
                    double num2 = (double)expression[i - 1];
                    double result = Calculate((char)expression[i], num1, num2);
                    expression.RemoveAt(i);
                    expression.RemoveAt(i - 1);
                    expression[i - 2] = result;
                    i -= 3;
                } 
            }
            Console.WriteLine(expression[0]);
            */
        }
    }
}
