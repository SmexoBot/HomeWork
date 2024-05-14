﻿using RpnLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpn.Logic
{
     class Lg : Operation
    {
        public override string Name => "lg";
        public override int Priority => 3;
        public override bool IsFunction => true;
        public override int ArgsNumber => 1;

        public override Numbers Execute(params Numbers[] numbers)
        {
            double num1 = numbers[0].Number;
            Numbers result = new Numbers(Math.Log10(num1));
            return result;
        }

    }
}