using System;

namespace Domain.Exceptions
{
    public class InvalidCoinException : Exception
    {
        public InvalidCoinException(string input) : base($"Invalid coin: {input}")
        {
        }
    }
}
