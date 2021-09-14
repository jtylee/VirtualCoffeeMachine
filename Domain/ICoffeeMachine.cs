using Domain.Models;
using Domain.ValueObjects;
using System.Collections.Generic;

namespace Domain
{
    public interface ICoffeeMachine
    {
        void AddCoin(string input);
        decimal GetBalance();
        Coffee PurchaseCoffee(string id);
        Coffee PurchaseCoffee(int id);
        List<Coin> CancelTransaction();
        List<Coin> GetChange();
    }
}
