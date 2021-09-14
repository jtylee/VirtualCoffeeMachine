using Domain.Models;
using System;

namespace Domain.Exceptions
{
    public class InsufficientBalanceException : Exception
    {
        public InsufficientBalanceException(Coffee coffee, decimal balance)
            : base($"Insufficient funds ({balance}) to purchase {coffee.Name} ({coffee.Price}).")
        {
        }
    }
}
