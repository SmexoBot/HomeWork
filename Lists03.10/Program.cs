using Microsoft.Win32;
using System;
using System.Data.Common;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using RpnLogic;

namespace RpnConsole
{
    public static class Program
    {
        public static void Main()
        {
            string inp = Console.ReadLine();
            RpnCulculator rpnCul = new RpnCulculator();
            double result = rpnCul.RpnCulculate(inp);
            Console.WriteLine(result);
        }
    }
}
