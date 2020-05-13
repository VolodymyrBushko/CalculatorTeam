using System;

namespace AnalaizerClass.Exceptions
{
    public class Error08 : Exception
    {
        public override string Message => "Error08 : The total number of numbers and operators exceeds 30.";
    }
}