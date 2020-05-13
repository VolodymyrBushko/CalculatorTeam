using System;

namespace AnalaizerClass.Exceptions
{
    public class Error02 : Exception
    {
        private int positionError;

        public Error02(int position) => positionError = position;

        public override string Message => $"Error02 : Unknown operator on {positionError} character.";
    }
}