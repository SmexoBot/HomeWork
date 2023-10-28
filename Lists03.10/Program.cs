using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace Lists
{
    public static class Program
    {
        public static int Registers(string inp)
        {
            int register1 = 0;
            int len = inp.Length;
            for (int i = 0; i < len; i++)
            {
                if (inp[i] == '(')
                {
                    register1++;
                }
            }
            return register1;
        }

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

        public static List<char> NewLists(int inp)
        {
            List<char> list = new List<char>(inp);
            return list;
        }
        public static List<List<char>> Operation(string inp)
        {
            List<List<char>> op = new List<List<char>>(4);
            inp += ' ';
            int len = inp.Length - 1;
            int register = 0;
            int register1 = Registers(inp) + 1;

            for (int i = 0; i < len; i++)
            {
                if (inp[i] == '(')
                {
                    register1++;
                }
            }

            
            for (int i = 0; i < register1; i++)
            {
                op.Add(NewLists(i));      
            }

            for (int i = 0; i < len; i++)
            {
                if ((Char.IsDigit(inp[i]) == false) && (inp[i] != '.') && (inp[i] != ' '))
                {
                    
                    if (inp[i] == '(')
                    {
                        register++;
                    }
                    if (inp[i] == '*' || inp[i] == '/')
                    {
                        
                        op[register].Add(inp[i]);
                        
                    }
                    if (inp[i] == '+' || inp[i] == '-')
                    {
                        op[register].Add(inp[i]);
                    }
                    if (inp[i] == ')')
                    {
                        register--;
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
            List<List<char>> operations = Operation(input);
            for (int i = operations.Count - 1; i >= 0; i--)
            {
                while (operations[i].Count > 0)
                {
                    int operIndex = -1;
                    if (operations[i].Contains('*') || operations[i].Contains('/'))
                    {
                        int multipleIndex = operations[i].IndexOf('*');
                        int devideIndex = operations[i].IndexOf('/');
                        operIndex = devideIndex != -1 && multipleIndex != -1
                            ? multipleIndex > devideIndex
                                ? devideIndex
                                : multipleIndex
                            : devideIndex == -1
                                ? multipleIndex
                                : devideIndex;
                    }
                    else
                    {
                        if (operations[i].Contains('+'))
                        {
                            operIndex = operations[i].IndexOf('+');
                        }
                        else
                        {
                            operIndex = operations[i].IndexOf('-');
                        }
                    }

                    if (operIndex == -1)
                    {
                        operIndex = 0;
                    }
                    
                    char op = operations[i][operIndex];
                    double num1 = numbers[operIndex + i];
                    double num2 = numbers[operIndex + 1 + i];
                    double result = Calculate(op, num1, num2);
                    numbers.RemoveAt(operIndex + 1 + i);
                    numbers.RemoveAt(operIndex + i);
                    operations[i].RemoveAt(operIndex);
                    numbers.Insert(operIndex + i, result);
                }
            }
                Console.WriteLine(numbers[0]);
        }
    }
}
