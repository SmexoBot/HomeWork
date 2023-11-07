using Microsoft.Win32;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace Lists
{
    public static class Program
    {
        public static List<object> ToRpn(string inp)
        {
            inp = inp + ' ';
            inp = inp.Replace('.', ',');
            int len = inp.Length - 1;
            List<object> num = new List<object>();
            Stack<char> op = new Stack<char>();
            op.Push(' ');

            for (int i = 0; i < len; i++)
            {
                if (inp[i] == ' ')
                {
                    continue;
                }
                else if (Char.IsDigit(inp[i]))
                {
                    int index = 1;
                    while (true)
                    {
                        if (Char.IsDigit(inp[i + index]) || inp[i + index] == ',')
                        {
                            index++;
                        }
                        else
                        {
                            num.Add(Convert.ToDouble(inp.Substring(i, index)));
                            i = i + index - 1;
                            break;
                        }
                    }

                }
                else
                {
                    if (op.Peek() == ' ')
                    {
                        op.Push(inp[i]);
                    }

                    else if (inp[i] == '(')
                    {
                        op.Push(inp[i]);
                    }

                    else if ((op.Peek() == '+' || op.Peek() == '-') && (inp[i] == '+' || inp[i] == '-'))
                    {
                        num.Add(op.Pop());
                        op.Push(inp[i]);
                    }
                   
                    else if ((op.Peek() == '*' || op.Peek() == '/'))
                    {
                        if ((inp[i] != '*' && inp[i] != '/'))
                        {
                            while (op.Peek() != '+' && op.Peek() != '-' && op.Peek() != ' ')
                            {
                                num.Add(op.Pop());
                            }
                            num.Add(op.Pop());
                            op.Push(inp[i]);
                        }
                        else if (inp[i] == '*' || inp[i] == '/')
                        {
                            num.Add(op.Pop());
                            op.Push(inp[i]);
                        }
                    }

                    else if (inp[i] == ')')
                    {
                        while (op.Peek() != '(')
                        {
                            num.Add(op.Pop());
                        }
                        op.Pop();
                    }

                    else
                    {
                        op.Push(inp[i]);
                    }
                }   
            }
            while (op.Count != 1)
            {
                num.Add(op.Pop());
            }
            return num;

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
            List<object> expression = ToRpn(inp);
            PrintList(expression);
            char check = ' ';
            
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
        }
    }
}
