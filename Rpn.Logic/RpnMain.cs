using Rpn.Logic;
using System;

namespace RpnLogic
{
    public class RpnCulculator
    {
        public double RpnCulculate(string inp, double variableD)
        {
            List<Token> list = ToRpn(GetList(inp), variableD);
            if (inp[0] == '-')
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
                    Numbers number = new Numbers();
                    number.Number = Convert.ToDouble(str);
                    list.Add(number);
                    i = i + str.Length - 1;
                }
                else if (char.IsLetter(inp[i]))
                {
                    Variable varible = new Variable();
                    varible.varible = inp[i];
                    list.Add(varible);
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

        private  Operation GetOperation(char inp)
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

        private  Parenthesis GetParenthesis(char inp)
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

        private  string GetNumbers(int i, int index, string inp)
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

        private  List<Token> ToRpn(List<Token> inp, double variable)
        {
            List<Token> list = new List<Token>();
            Stack<Token> op = new Stack<Token>();
            int len = inp.Count();
            for (int i = 0; i < len; i++)
            {
                if (inp[i] is Variable)
                {
                    Numbers variabl = new Numbers();
                    variabl.Number = variable;
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

        private  double Calculate(char op, double num1, double num2)
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

        private  double GetResult(List<Token> expression)
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
                    double num1;
                    if (!(number.Count == 0))
                    {
                        num1 = number.Pop().Number;
                    }
                    else 
                    {
                        num1 = 0;
                    }
                    char op = ((Operation)expression[i]).Symbol;
                    Numbers res = new Numbers();
                    res.Number = Calculate(op, num1, num2);
                    number.Push(res);
                }
            }
            return number.Pop().Number;
        }
    }
}