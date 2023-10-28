using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace Lists
{
    public static class Program
    {
        public static List<double> Number(string inp)
        {
            List<double> num = new List<double>();
            inp += ' ';
            inp = inp.Replace('.', ',');
            int len = inp.Length - 1;
            for (int i = 0; i < len; i++)
            {
                if (Char.IsDigit(inp[i]))
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
                    continue;
                }

            }
            return num;
        }

        public static List<(char,int)> Operation(string inp)
        {
            List<(char,int)> op = new List<(char,int)>();
            inp += ' ';
            int len = inp.Length - 1;
            int register = 0;
            
            for (int i = 0; i < len; i++)
            {
                if ((Char.IsDigit(inp[i]) == false) && (inp[i] != '.') && (inp[i] != ' '))
                {
                    
                    if (inp[i] == '(')
                    {
                        register += 2;
                    }
                    if (inp[i] == '*' || inp[i] == '/')
                    {
                        op.Add((inp[i], register + 1));
                    }
                    if (inp[i] == '+' || inp[i] == '-')
                    {
                        op.Add((inp[i], register));
                    }
                    if (inp[i] == ')')
                    {
                        register -= 2;
                    }
                }
            }
            return op;
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
            string input = Console.ReadLine();
            List<double> numbers = Number(input);
            List<(char,int)> operations = Operation(input);
            while (operations.Count > 0)
            {
                int operIndex = -1;
                int max = 0;
                for (int i = 0; i < operations.Count; i++)
                {
                    if (max < operations[i].Item2)
                    {
                        max = operations[i].Item2;
                        operIndex = i;
                    }
                    if (max == operations[i].Item2)
                    {
                        if (operations[i].Item1 == '*' || operations[i].Item1 == '/')
                        {
                            operIndex = i;
                        }
                    }
                }

                if (operIndex == -1)
                {
                        operIndex = 0;
                }
                    
                char op = operations[operIndex].Item1;
                double num1 = numbers[operIndex];
                double num2 = numbers[operIndex + 1];
                double result = Calculate(op, num1, num2);
                numbers.RemoveAt(operIndex + 1);
                numbers.RemoveAt(operIndex);
                operations.RemoveAt(operIndex);
                numbers.Insert(operIndex, result);
                
            }
            Console.WriteLine(numbers[0]);
        }
    }
}
