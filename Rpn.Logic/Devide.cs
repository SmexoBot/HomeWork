using RpnLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpn.Logic
{
     class Devide : Operation
    {
        public override string Name => "/";
        public override int Priority => 2;
        public override bool IsFunction => false;
        public override int ArgsNumber => 2;

        public override Numbers Execute(params Numbers[] numbers)
        {
            double num2 = numbers[0].Number;
            double num1 = numbers[1].Number;
            Numbers result = new Numbers(num1 / num2);
            return result;
        }

    }
}
