using System; 

namespace CalcClassDll.Exceptions
{
    public class Error06: Exception
    {
        public override string Message => "Error06 : Very small or very large value of the number";
    }
}
