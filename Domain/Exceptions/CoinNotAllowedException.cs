using Domain.ValueObjects;
using System;

namespace Domain.Exceptions
{
    public class CoinNotAllowedException : Exception
    {
        public CoinNotAllowedException(Coin coin) : base($"Coin not allowed: {coin.ValueInCents}")
        {
        }
    }
}
