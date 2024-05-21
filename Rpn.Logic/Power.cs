using RpnLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpn.Logic
{
     class Power : Operation
    {
        public override string Name => "^";
        public override int Priority => 3;
        public override bool IsFunction => false;
        public override int ArgsNumber => 2;
        public override double MinValue => double.MinValue;
        public override double MaxValue => double.MaxValue;
        public override double NotEqual => double.MaxValue;

        public override Numbers Execute(params Numbers[] numbers)
        {
            double num1 = numbers[0].Number;
            double num2 = numbers[1].Number;
            return new Numbers(Math.Pow(num1, num2));   
        }

    }
}
