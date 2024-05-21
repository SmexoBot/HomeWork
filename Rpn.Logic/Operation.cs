
namespace RpnLogic
{
    abstract class Operation : Token
    {
        public abstract string Name { get; }
        public abstract int Priority { get; }
        public abstract bool IsFunction { get; }
        public abstract int ArgsNumber { get; }
        public abstract double MinValue { get; }
        public abstract double MaxValue { get; }
        public abstract double NotEqual { get; }
        private int Priority0;

        private void GetPriority(int priority)
        {
            Priority0 = priority;
        }

        public Operation ChangePriority(int priority, Operation op)
        {
            op.GetPriority(priority);
            return(op);
        }

        public abstract Numbers Execute(params Numbers[] numbers);

        public static bool operator >=(Operation left, Operation right)
        {
            return left.Priority + left.Priority0 >= right.Priority + right.Priority0;
        }

        public static bool operator <=(Operation left, Operation right)
        {
            return (left.Priority + left.Priority0 <= right.Priority + right.Priority0);
        }
    }
}