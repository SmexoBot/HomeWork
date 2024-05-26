using Rpn.Logic;
using System;
using System.ComponentModel.Design;
using System.Reflection;

namespace RpnLogic
{
    public class RpnCulculator
    {
        private static List<Operation> allOperations; 
        private readonly string equation;

        public RpnCulculator(string input) 
        { 
            equation = input.Trim().ToLower();
        }

        public double RpnCulculate( double variableD)
        {
            List<Token> list = ToRpn(GetList(equation), variableD);
            if (equation[0] == '-')
            {
                return -1*GetResult(list);
            }
            else
            {
                return GetResult(list);
            }
        }

        private List<Token> GetList(string inp)
        {
            inp = inp + ' ';
            inp = inp.Replace('.', ',');
            int i;
            if (inp[0] == '-')
            {
                i=1;
            }
            else
            {
                i=0;
            }
            int priority = 0;
            int len = inp.Length - 1;
            List<Token> list = new List<Token>();
            for ( ; i < len; i++)
            {
                if (inp[i] == ' ')
                {
                    continue;
                }
                else if (char.IsDigit(inp[i]))
                {
                    string str = GetNumbers(i, 1, inp);
                    Numbers number = new Numbers(str);
                    list.Add(number);
                    i = i + str.Length - 1;
                }
                else if (char.IsLetter(inp[i]))
                { 
                    int index = 0;
                    int parenthesisNumber = 0;
                    int numberLetter = i;
                    while (true)
                    {  
                         if (inp[i + index] == 'x' || inp[i + index] == 'y' || inp[i + index] == 'z')
                        {
                            Variable varible = new Variable(inp[i]);
                            list.Add(varible);
                            index++;
                            numberLetter++;
                         }
                        else if (char.IsDigit(inp[i + index]))
                        {
                            string str = GetNumbers(i + index, 1, inp);
                            Numbers number = new Numbers(str);
                            list.Add(number);
                            index += str.Length;
                            numberLetter += str.Length;
                        }
                        else if (inp[i + index] == ')' && parenthesisNumber == 1)
                        {
                            priority--;
                            parenthesisNumber--;
                            i += index + 1;
                            break;
                        }
                        else if (inp[i + index] == ')' && parenthesisNumber != 0)
                        {
                            priority--;
                            parenthesisNumber--;
                            index++;
                            numberLetter++;
                        }
                        else if (inp[i + index] == ';')
                        {
                            index++;
                            if (char.IsDigit(inp[i + index]))
                            {
                                string str = GetNumbers(i + index, 1, inp);
                                Numbers numbers = new Numbers(str);
                                list.Add(numbers);
                                index += str.Length;
                                numberLetter += str.Length;
                            }
                            else
                            {
                                list.Add(new Variable(inp[i + index]));
                                index++;
                                numberLetter++;
                            }
                        }
                        else if (char.IsLetter(inp[i + index]) )
                        {
                            index++;
                        }
                         else if ((inp[i + index] == '('))
                        {
                            parenthesisNumber++;
                            Operation op = CreateOperation(inp.Substring(numberLetter, index - numberLetter));
                            op = op.ChangePriority(priority, op);
                            list.Add(op);
                            index++;
                            numberLetter = index;
                            priority++;
                         }
                        else
                        {
                            index++;
                            Operation op = CreateOperation(inp.Substring(numberLetter, index - numberLetter));
                            op = op.ChangePriority(priority, op);
                            list.Add(op);
                            numberLetter = index;
                        }
                    }                
                }
                else
                {
                    if (inp[i] == '(' || inp[i] == ')')
                    {
                        if(inp[i] == '(')
                        {
                            priority++;
                        }
                        else
                        {
                            priority--;
                        }
                            list.Add(new Parenthesis(inp[i]));
                        if (inp[i] == '(' && inp[i + 1] == '-')
                        {
                            list.Add(new Numbers(0));
                        }
                    }
                    else
                    {
                        Operation op = CreateOperation(inp[i].ToString());
                        op = op.ChangePriority(priority + 3, op);
                        list.Add(op);
                    }
                }
            }
            return list;
        }

        private  Operation CreateOperation(string name)
        {
            var op =  FindAvaliableOperattionByName(name);
            if (op == null)
            {
                throw new ArgumentException($"Unknow operation {name}");
            }
            
            return op;
        }

        private Operation FindAvaliableOperattionByName(string name)
        {
            if (allOperations == null)
            {
                Type parent = typeof(Operation);
                Assembly[] allAssemblies = AppDomain.CurrentDomain.GetAssemblies();
                var types = allAssemblies.SelectMany(x => x.GetTypes());
                List<Type> inheritingTypes = types.Where(t => parent.IsAssignableFrom(t) && !t.IsAbstract).ToList();

                allOperations = inheritingTypes.Select(type => (Operation)Activator.CreateInstance(type)).ToList();
            }
            return allOperations.FirstOrDefault(op => op.Name.Equals(name));
        }

        private  string GetNumbers(int i, int index, string inp)
        {
            while (true)
            {
                if (char.IsDigit(inp[i + index]) || inp[i + index] == ',')
                {
                    index++;
                }
                else
                {
                    return inp.Substring(i, index);
                }
            }
        }

        private  List<Token> ToRpn(List<Token> inp, double variable)
        {
            List<Token> list = new List<Token>();
            Stack<Token> op = new Stack<Token>();
            int len = inp.Count();
            for (int i = 0; i < len; i++)
            {
                if (inp[i] is Variable)
                {
                    Numbers variabl = new Numbers(variable);
                    list.Add(variabl);
                }
                else if (inp[i] is Numbers)
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
                            while (op.Count() != 0)
                            { 
                                if ((Operation)op.Peek() >= operation)
                                {
                                    list.Add(op.Pop());
                                }
                                else
                                {
                                    break;
                                }
                            }
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

        private  double GetResult(List<Token> expression)
        {
            Stack<Numbers> number = new Stack<Numbers>();

            for (int i = 0; i < expression.Count(); i++)
            {
                Token tocen = expression[i];
                if (tocen is Numbers)
                {
                    Numbers num = tocen as Numbers;
                    if (num.Number == 0)
                    {
                        number.Push(new Numbers(0));
                    }
                    else
                    {
                        number.Push((Numbers)expression[i]);
                    }
                }
                else if (tocen is Operation)
                {
                    Operation op = tocen as Operation;
                    Numbers[] args = new Numbers[op.ArgsNumber];
                    for (int j = op.ArgsNumber - 1; j >= 0; j--)
                    {
                        if (op.IsFunction)
                        {
                            if(number.Peek().Number <= op.MinValue || number.Peek().Number >= op.MaxValue || number.Peek().Number == op.NotEqual)
                            {
                                return 999;
                            }
                        }
                        
                        args[j] = number.Pop();
                    }
                    Numbers result = op.Execute(args);
                    number.Push(result);
                }
            }
            return number.Pop().Number;
        }
    }
}