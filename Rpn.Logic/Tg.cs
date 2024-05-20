using RpnLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpn.Logic
{
     class Tg : Operation
    {
        public override string Name => "tg";
        public override int Priority => 3;
        public override bool IsFunction => true;
        public override int ArgsNumber => 1;
        public override double MinValue => double.MinValue;
        public override double MaxValue => double.MaxValue;
        public override double NotEqual => Math.PI / 2;


        public override Numbers Execute(params Numbers[] numbers)
        {
            double num1 = numbers[0].Number;
            return new Numbers(Math.Tan(num1));
        }

    }
}
