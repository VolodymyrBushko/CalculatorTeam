using System;

namespace AnalaizerClass.Exceptions
{
    public class Error03 : Exception
    {
        public override string Message => "Error03 : Incorrect syntactic construction of the input expression.";
    }
}