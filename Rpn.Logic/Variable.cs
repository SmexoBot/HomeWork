using RpnLogic;


namespace Rpn.Logic 
{
    public class Variable : Token 
    {
        public char varible { get; }

        public Variable(char name)
        {
            varible = name;
        }
    }
}