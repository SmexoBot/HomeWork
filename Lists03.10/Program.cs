using Microsoft.Win32;
using System;
using System.Data.Common;
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
        public int Priority;
        public char Symbol;
        public static bool operator>=(Operation op1, Operation op2)
        {
            return op1.Priority >= op2.Priority;
        }
        
        public static bool operator<=(Operation op1, Operation op2)
        {
            return op1.Priority <= op2.Priority;
        }
    }

    public class Parenthesis : Tocen
    {
        public bool IsOpen;
    }

    public class Numbers : Tocen
    {
        public double Number;
    }

    public static class Program
    {
        public static List<Tocen> GetList(string inp)
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
                    string str = GetNumbers(i, 1, inp);
                    Numbers num = new Numbers();
                    num.Number = Convert.ToDouble(str);
                    list.Add(num);
                    i = i + str.Length - 1;
                }
                else
                {
                    if (inp[i] == '(' || inp[i] == ')')
                    {
                        list.Add(GetParenthesis(inp[i]));
                    }
                    else
                    {
                        list.Add(GetOperation(inp[i]));
                    }
                }
            }
            return list;
        }

        public static Operation GetOperation(char inp)
        {
            Operation op = new Operation();
            if (inp == '*')
            {
                op.Symbol = inp;
                op.Priority = 1;
            }
            else if (inp == '/')
            {
                op.Symbol = '/';
                op.Priority = 1;
            }
            else if (inp == '+')
            {
                op.Symbol = '+';
            }
            else
            {
                op.Symbol = '-';
            }
            return op;
        }

        public static Parenthesis GetParenthesis(char inp)
        {
            Parenthesis parenthesis = new Parenthesis();
            if (inp == '(')
            {
                parenthesis.IsOpen = true;
            }
            else
            {
                parenthesis.IsOpen = false;
            }
            return parenthesis;
        }

        public static string GetNumbers(int i, int index, string inp)
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
            List<Tocen> list = new List<Tocen>();
            Stack<Tocen> op = new Stack<Tocen>();
            int len = inp.Count();

            for (int i = 0; i < len; i++)
            {
                if (inp[i] is Numbers)
                {
                    list.Add(inp[i]);
                }
                else
                {
                    if (inp[i] is Parenthesis)
                    {
                        if (((Parenthesis)inp[i]).IsOpen)
                        {
                            op.Push(inp[i]);
                        }
                        else
                        {
                            while (op.Peek() is Operation)
                            {
                                list.Add(op.Pop());
                            }
                            op.Pop();
                        }
                    }
                    else if (op.Count() > 0 && op.Peek() is Parenthesis)
                    {
                        op.Push(inp[i]);
                    }
                    else
                    {
                        Operation operation = (Operation)inp[i];
                        if (op.Count() == 0)
                        {
                            op.Push(inp[i]);
                        }
                        else if ((Operation)op.Peek() >= operation)
                        {
                            list.Add(op.Pop());
                            op.Push(operation);
                        }
                        else
                        {
                            op.Push(operation);
                        }
                    }
                }
            }
            while (op.Count() > 0)
            {
                list.Add(op.Pop());
            }
            return list;
        }

        public static void PrintList(List<Tocen> list)
        {
            foreach (Tocen o in list)
            {
                if (o is Numbers)
                {
                    Console.Write(((Numbers)o).Number);
                }
                else
                {
                    Console.Write(((Operation)o).Symbol);
                }
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

        public static double GetResult(List<Tocen> expression)
        {
            Stack<Numbers> number = new Stack<Numbers>();

            for (int i = 0; i < expression.Count(); i++)
            {
                if (expression[i] is Numbers)
                {
                    number.Push((Numbers)expression[i]);
                }
                else if (expression[i] is Operation)
                {
                    double num2 = number.Pop().Number;
                    double num1 = number.Pop().Number;
                    char op = ((Operation)expression[i]).Symbol;

                    Numbers res = new Numbers();
                    res.Number = Calculate(op, num1, num2);
                    number.Push(res);
                }
            }
            return number.Pop().Number;
        }

        public static void Main()
        {
            string inp = Console.ReadLine();
            List<Tocen> expression = ToRpn(GetList(inp));
            PrintList(expression);
            double result = GetResult(expression);
            Console.WriteLine(result);
        }
    }
}
