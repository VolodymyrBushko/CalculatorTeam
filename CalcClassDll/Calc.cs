using CalcClassDll.Exceptions;

namespace CalcClassDll
{
    public static class Calc
    {
        public static string LastError { get; set; }

        public static double Add(double x, double y)
        {
            return (x + y) <= double.MaxValue ? (x + y) : throw new Error06();
        }

        public static double Sub(double x, double y)
        {
            return (x - y) >= double.MinValue ? (x - y) : throw new Error06();
        }

        public static double Mult(double x, double y)
        {
            return (x * y) <= double.MaxValue ? (x * y) : throw new Error06();
        }

        public static double Div(double x, double y)
        {
            return y != 0 ? x / y : throw new Error09();
        }

        public static double Mod(double x, double y)
        {
            return ((x + y) <= double.MaxValue && (x - y) >= double.MinValue) ? x % y : throw new Error06();
        }

        public static double ABS(double x)
        {
            if (x > double.MinValue && x <= double.MaxValue)
                return x >= 0 ? x : -x;
            else
                throw new Error06();
        }

        public static double IABS(double x)
        {
            return (x > double.MinValue && x <= double.MaxValue) ? -x : throw new Error06(); ;
        }

    }
}