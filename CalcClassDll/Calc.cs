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
                return x + y;
            }
            catch (Exception e)
            {
                LastError = e.Message;
                throw e;
            }
        }

        public static double Sub(double x, double y)
        {
            try
            {
                return x - y;
            }
            catch (Exception e)
            {
                LastError = e.Message;
                throw e;
            }
        }
        public static double Mult(double x, double y)
        {
            try
            {
                return x * y;
            }
            catch (Exception e)
            {
                LastError = e.Message;
                throw e;
            }
        }
        public static double Div(double x, double y)
        {
            try
            {
                return x / y;
            }
            catch (Exception e)
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
            catch (Exception e)
            {
                LastError = e.Message;
                throw e;
            }
        }

        public static double ABS(double x)
        {
            try
            {
                return x >= 0 ? x : -x;
            }
            catch (Exception e)
            {
                LastError = e.Message;
                throw e;
            }
        }

        public static double IABS(double x)
        {
            try
            {
                return -x;
            }
            catch (Exception e)
            {
                LastError = e.Message;
                throw e;
            }
        }

    }
}
