using System;

namespace AnalaizerClass.Exceptions
{
    public class Error01 : Exception
    {
        private int positionError;

        public Error01(int position) => positionError = position;

        public override string Message => $"Error01 : Incorrect structure in parentheses, error on {positionError} position.";
    }
}