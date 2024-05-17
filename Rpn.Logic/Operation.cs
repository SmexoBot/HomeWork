
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

        public abstract Numbers Execute(params Numbers[] numbers);

        public static bool operator >=(Operation left, Operation right)
        {
            return left.Priority >= right.Priority;
        }

        public static bool operator <=(Operation left, Operation right)
        {
            return (left.Priority <= right.Priority);
        }
    }
}