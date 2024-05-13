

namespace RpnLogic
{
    public class Parenthesis : Token
    {
        public readonly bool IsOpen;

        public Parenthesis(char inp)
        {
            if (inp == '(')
            {
                IsOpen = true;
            }
            else
            {
                IsOpen = false;
            }
        }

    }
}