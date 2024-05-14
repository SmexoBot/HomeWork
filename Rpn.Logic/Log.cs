using RpnLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpn.Logic
{
     class Log : Operation
    {
        public override string Name => "log";
        public override int Priority => 3;
        public override bool IsFunction => true;
        public override int ArgsNumber => 2;

        public override Numbers Execute(params Numbers[] numbers)
        {
            double num1 = numbers[0].Number;
            double num2 = numbers[1].Number;
            Numbers result = new Numbers(Math.Log(num1, num2));
            return result;
        }

    }
}
