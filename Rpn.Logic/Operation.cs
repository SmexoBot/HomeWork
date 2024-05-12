
namespace RpnLogic
{
    abstract class Operation : Token
    {
        public abstract string Name { get; }
        public abstract int Priority { get; }
        public abstract bool IsFunction { get; }
        public abstract int ArgsNumber { get; }

        public abstract Numbers Execute(params Numbers[] numbers);

    }
}