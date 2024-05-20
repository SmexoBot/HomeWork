using RpnLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpn.Logic
{
     class Ln : Operation
    {
        public override string Name => "ln";
        public override int Priority => 3;
        public override bool IsFunction => true;
        public override int ArgsNumber => 1;
        public override double MinValue => 0;
        public override double NotEqual => double.MinValue;
        public override double MaxValue => double.MaxValue;
        

        public override Numbers Execute(params Numbers[] numbers)
        {
            double num1 = numbers[0].Number;
            return new Numbers(Math.Log(num1));
        }

    }
}
