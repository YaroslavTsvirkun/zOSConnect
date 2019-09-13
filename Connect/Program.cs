using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace zConnect
{
    class Program
    {
        static void Main(string[] args)
        {
            Double x = double.MaxValue;
            Double y = 0;
            y = GetFrexp(x, out Int32 n);

            Console.WriteLine($"{y.ToString("R")} = frexp({x}, 2^{n})");
            Console.ReadKey();
        }

        public static Double GetFrexp(Double value, out Int32 exponent)
        {
            return Frexp(value, out exponent);
        }

        [DllImport("Math.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        private extern static Double Frexp([MarshalAs(UnmanagedType.R8)] Double value, 
                                           [MarshalAs(UnmanagedType.I4)] out Int32 exponent);
    }
}
