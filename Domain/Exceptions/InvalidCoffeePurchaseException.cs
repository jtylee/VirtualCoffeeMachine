using System;

namespace Domain.Exceptions
{
    public class InvalidCoffeePurchaseException : Exception
    {
        public InvalidCoffeePurchaseException(string id) : base($"Invalid coffee selection: {id}")
        {
        }
    }
}
