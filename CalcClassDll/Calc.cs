using CalcClassDll.Exceptions;
using System; 

namespace CalcClassDll
{
    public static class Calc
    {
        public static string LastError { get; set; }

        public static double Add(double x, double y)
        {
            try
            {
                return (x + y) <= double.MaxValue ? (x + y) : throw new Error06(); 
            }
            catch (Error06 e)
            {
                LastError = e.Message;
                throw e;
            }
        }

        public static double Sub(double x, double y)
        {
            try
            {
                return (x - y) >= double.MinValue ? (x - y) : throw new Error06();
            }
            catch (Error06 e)
            {
                LastError = e.Message;
                throw e;
            }
        }
        public static double Mult(double x, double y)
        {
            try
            {
                return (x * y) <= double.MaxValue ? (x * y) : throw new Error06();
            }
            catch (Error06 e)
            {
                LastError = e.Message;
                throw e;
            }
        }
        public static double Div(double x, double y)
        {
            try
            {
                return y != 0 ? x / y : throw new Error09();
            }
            catch (Error09 e)
            {
                LastError = e.Message;
                throw e;
            }
        }

        public static double Mod(double x, double y)
        {
            try
            {
                return x % y;
            }
            catch (Error06 e)
            {
                LastError = e.Message;
                throw e;
            }
        }

        public static double ABS(double x)
        {
            try
            {
                if (x > double.MinValue && x <= double.MaxValue)
                {
                    return x >= 0 ? x : -x;
                }
                else
                {
                    throw new Error06();
                }
                
            }
            catch (Error06 e)
            {
                LastError = e.Message;
                throw e;
            }
        }

        public static double IABS(double x)
        {
            try
            {
                return (x > double.MinValue || x <= double.MaxValue) ? -x : throw new Error06(); ;
            }
            catch (Error06 e)
            {
                LastError = e.Message;
                throw e;
            }
        }

    }
}
