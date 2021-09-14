using Domain.Exceptions;
using Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Models
{
    public class CoffeeMachine : ICoffeeMachine
    {
        private decimal _balance = 0m;
        private readonly List<Coffee> _coffees;
        private readonly List<Coin> _allowedCoins;

        public CoffeeMachine(List<Coffee> coffees, List<Coin> allowedCoins)
        {
            _coffees = coffees;
            _allowedCoins = allowedCoins;
        }

        public void AddCoin(string input)
        {
            if (!int.TryParse(input, out int amt))
                throw new InvalidCoinException(input);

            var coin = new Coin(amt);

            if(!coin.IsValid())
                throw new InvalidCoinException(input);

            if (!_allowedCoins.Any(c => c.ValueInCents == coin.ValueInCents))
                throw new CoinNotAllowedException(coin);

            _balance += coin.ValueInDollars;
        }

        public decimal GetBalance() => _balance;

        public Coffee PurchaseCoffee(string id)
        {
            if (!int.TryParse(id, out int coffeeId))
                throw new InvalidCoffeePurchaseException(id);

            return PurchaseCoffee(coffeeId);
        }

        public Coffee PurchaseCoffee(int id)
        {
            var coffee = _coffees.Single(x => x.Id == id);

            if (_balance < coffee.Price)
                throw new InsufficientBalanceException(coffee, _balance);

            _balance -= coffee.Price;

            return coffee;
        }

        public List<Coin> CancelTransaction()
        {
            var change = GetChange();
            _balance = 0; // jic
            return change;
        }

        public List<Coin> GetChange()
        {
            var change = new List<Coin>();

            while (_balance > 0)
            {
                var largestCoin = _allowedCoins
                    .Where(coin => coin.ValueInDollars <= _balance)
                    .OrderByDescending(coin => coin.ValueInDollars)
                    .FirstOrDefault();

                if (largestCoin == null) break;

                change.Add(largestCoin);
                _balance -= largestCoin.ValueInCents;
            }

            return change;
        }

    }
}
