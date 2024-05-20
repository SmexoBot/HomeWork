using RpnLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpn.Logic
{
     class Cos : Operation
    {
        public override string Name => "cos";
        public override int Priority => 3;
        public override bool IsFunction => true;
        public override int ArgsNumber => 1;
        public override double MinValue => double.MinValue;
        public override double MaxValue => double.MaxValue;
        public override double NotEqual => double.MaxValue;


        public override Numbers Execute(params Numbers[] numbers)
        {
            double num1 = numbers[0].Number;
            return new Numbers(Math.Cos(num1));
        }

    }
}
