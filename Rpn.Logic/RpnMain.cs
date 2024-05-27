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

        private List<Token> GetList(string inputStr)
        {
            inputStr = inputStr + ' ';
            inputStr = inputStr.Replace('.', ',');
            int i;
            if (inputStr[0] == '-')
            {
                i=1;
            }
            else
            {
                i=0;
            }
            int priority = 0;
            int len = inputStr.Length - 1;
            List<Token> list = new List<Token>();
            for ( ; i < len; i++)
            {
                if ( inputStr[i] == ';' || inputStr[i] == ' ')
                {
                    continue;
                }
                else if (char.IsDigit(inputStr[i]))
                {
                    string str = GetNumbers(i, 1, inputStr);
                    Numbers number = new Numbers(str);
                    list.Add(number);
                    i = i + str.Length - 1;
                }
                else if (inputStr[i] == 'x' || inputStr[i] == 'y' || inputStr[i] == 'z')
                {
                    Variable varible = new Variable(inputStr[i]);
                    list.Add(varible);
                }
                else if (char.IsLetter(inputStr[i]))
                {
                    string str = GetOperation(inputStr, i);
                    Operation operation = CreateOperation(str);
                    operation = operation.ChangePriority(priority);
                    i += str.Length - 1;
                    list.Add(operation);
                }
                else
                {
                    if (inputStr[i] == '(' || inputStr[i] == ')')
                    {
                        if (inputStr[i] == '(')
                        {
                            priority++;
                        }
                        else
                        {
                            priority--;
                        }
                        list.Add(new Parenthesis(inputStr[i]));
                        if (inputStr[i] == '(' && inputStr[i + 1] == '-')
                        {
                            list.Add(new Numbers(0));
                        }
                    }
                    else
                    {
                        Operation operation = CreateOperation(inputStr[i].ToString());
                        operation = operation.ChangePriority(priority);
                        list.Add(operation);
                    }
                }
            }
            return list;
        }

        private string GetOperation(string inputStr, int start)
        {
            int index = 0;
            while (true)
            {
                if (char.IsLetter(inputStr[start + index]))
                {
                    index++;
                }
                else
                {
                    return inputStr.Substring(start, index);
                }
            }
        }

        private  Operation CreateOperation(string name)
        {
            var operation =  FindAvaliableOperattionByName(name);
            if (operation == null)
            {
                throw new ArgumentException($"Unknow operation {name}");
            }
            
            return operation;
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

        private  string GetNumbers(int i, int index, string inputStr)
        {
            while (true)
            {
                if (char.IsDigit(inputStr[i + index]) || inputStr[i + index] == ',')
                {
                    index++;
                }
                else
                {
                    return inputStr.Substring(i, index);
                }
            }
        }

        private  List<Token> ToRpn(List<Token> inputList, double variable)
        {
            List<Token> list = new List<Token>();
            Stack<Token> operationStack = new Stack<Token>();
            int len = inputList.Count();

            for (int i = 0; i < len; i++)
            {
                if (inputList[i] is Variable)
                {
                    Numbers variabl = new Numbers(variable);
                    list.Add(variabl);
                }
                else if (inputList[i] is Numbers)
                {
                    list.Add(inputList[i]);
                }
                else
                {
                    if (inputList[i] is Parenthesis)
                    {
                        if (((Parenthesis)inputList[i]).IsOpen)
                        {
                            operationStack.Push(inputList[i]);
                        }
                        else
                        {
                            while (operationStack.Peek() is Operation)
                            {
                                list.Add(operationStack.Pop());
                            }
                            operationStack.Pop();
                        }
                    }
                    else if (operationStack.Count() > 0 && operationStack.Peek() is Parenthesis)
                    {
                        operationStack.Push(inputList[i]);
                    }
                    else
                    {
                        Operation operation = (Operation)inputList[i];
                        if (operationStack.Count() == 0)
                        {
                            operationStack.Push(inputList[i]);
                        }
                        else if ((Operation)operationStack.Peek() >= operation)
                        {
                            while (operationStack.Count() != 0)
                            { 
                                if ((Operation)operationStack.Peek() >= operation)
                                {
                                    list.Add(operationStack.Pop());
                                }
                                else
                                {
                                    break;
                                }
                            }
                             operationStack.Push(operation);
                        }
                        else
                        {
                            operationStack.Push(operation);
                        }
                    }
                }
            }
            while (operationStack.Count() > 0)
            {
                list.Add(operationStack.Pop());
            }
            return list;
        }

        private  double GetResult(List<Token> expression)
        {
            Stack<Numbers> numberStack = new Stack<Numbers>();

            for (int i = 0; i < expression.Count(); i++)
            {
                Token tocen = expression[i];
                if (tocen is Numbers)
                {
                    Numbers number = tocen as Numbers;
                    if (number.Number == 0)
                    {
                        numberStack.Push(new Numbers(0));
                    }
                    else
                    {
                        numberStack.Push((Numbers)expression[i]);
                    }
                }
                else if (tocen is Operation)
                {
                    Operation operation = tocen as Operation;
                    Numbers[] args = new Numbers[operation.ArgsNumber];
                    for (int j = operation.ArgsNumber - 1; j >= 0; j--)
                    {
                        if (operation.IsFunction)
                        {
                            if(numberStack.Peek().Number <= operation.MinValue || numberStack.Peek().Number >= operation.MaxValue || numberStack.Peek().Number == operation.NotEqual)
                            {
                                return 999;
                            }
                        }
                        
                        args[j] = numberStack.Pop();
                    }
                    Numbers result = operation.Execute(args);
                    numberStack.Push(result);
                }
            }
            return numberStack.Pop().Number;
        }
    }
}