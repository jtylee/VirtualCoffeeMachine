using Domain.Exceptions;
using Domain.Models;
using Domain.ValueObjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Domain.Tests
{
    public class CoffeeMachineTests
    {
        private ICoffeeMachine _sut;
        private readonly static List<Coffee> _coffeeMenu = new()
        {
            new Coffee { Id = 1, Name = "Cappuccino", Price = 3.50m },
            new Coffee { Id = 2, Name = "Latte", Price = 3m },
            new Coffee { Id = 3, Name = "Decaf", Price = 4m },
        };

        private readonly static List<Coin> _allowedCoins = new()
        {
            new Coin(5),
            new Coin(10),
            new Coin(20),
            new Coin(50),
            new Coin(100),
            new Coin(200),
        };

        [SetUp]
        public void Setup()
        {
            _sut = new CoffeeMachine(_coffeeMenu, _allowedCoins);
        }

        #region AddCoin

        [TestCase(null)]
        [TestCase("")]
        [TestCase("0")]
        [TestCase("-1")]
        [TestCase("-2,147,483,648")] // int.MinValue
        [TestCase("2,147,483,647")] // int.MaxValue
        public void AddCoin_Should_Reject_Invalid_Coin(string amt)
        {
            Assert.Throws<InvalidCoinException>(() => _sut.AddCoin(amt));
        }

        [TestCase("5")]
        [TestCase("10")]
        [TestCase("20")]
        [TestCase("50")]
        [TestCase("100")]
        [TestCase("200")]
        public void AddCoin_Should_Accept_Valid_Coins(string amt)
        {
            _sut.AddCoin(amt);
            var balance = _sut.GetBalance();
            Assert.AreEqual(decimal.Parse(amt) / 100m, balance);
        }

        [TestCase("1")]
        [TestCase("2")]
        public void AddCoin_Should_Reject_NotAllowed_Coins(string amt)
        {
            Assert.Throws<CoinNotAllowedException>(() => _sut.AddCoin(amt));
        }

        #endregion
    }
}