


namespace RpnLogic
{
    public class Numbers : Token
    {
        public double Number { get; }

        public Numbers(string number) 
        { 
            Number = Convert.ToDouble(number);
        }

        public Numbers(double number)
        {
            Number = number;
        }
    }
}