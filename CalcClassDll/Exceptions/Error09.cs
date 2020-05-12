using System; 

namespace CalcClassDll.Exceptions
{
    public class Error09:Exception
    {
        public override string Message => "Error09 : divided by zero";
    }
}
