using System.Collections.Generic;
using System.Linq;

namespace Domain.ValueObjects
{
    public class Coin
    {
        private readonly List<int> _validCoinValues = new() { 1, 2, 5, 10, 20, 50, 100, 200 };

        public Coin(int valueInCents)
        {
            ValueInCents = valueInCents;
        }

        public int ValueInCents { get; init; }

        public decimal ValueInDollars => ValueInCents / 100m;

        public bool IsValid() => _validCoinValues.Any(x => x == ValueInCents);
    }
}
