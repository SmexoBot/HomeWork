
namespace RpnLogic
{
    public class Operation : Tocen
    {
        public int Priority;
        public char Symbol;
        public static bool operator >=(Operation op1, Operation op2)
        {
            return op1.Priority >= op2.Priority;
        }

        public static bool operator <=(Operation op1, Operation op2)
        {
            return op1.Priority <= op2.Priority;
        }
    }
}