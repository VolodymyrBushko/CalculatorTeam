using System;

namespace AnalaizerClass.Exceptions
{
    public class Error04 : Exception
    {
        private int positionError;

        public Error04(int position) => positionError = position;

        public override string Message => $"Error04 : Two consecutive operators on the {positionError} character";
    }
}