using Domain.Models;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App
{
    class Program
    {
        private readonly static List<Coffee> _coffeeMenu = new()
        {
            new Coffee { Id = 1, Name = "Cappuccino", Price = 3.50m},
            new Coffee { Id = 2, Name = "Latte", Price = 3m},
            new Coffee { Id = 3, Name = "Decaf", Price = 4m},
        };

        private readonly static List<Coin> _allowedCoins = new()
        {
            new Coin(5), new Coin(10), new Coin(20), new Coin(50), new Coin(100), new Coin(200),
        };

        static void Main(string[] args)
        {
            var coffeeMachine = new CoffeeMachine(_coffeeMenu, _allowedCoins);
            Console.WriteLine("Coffee Machine started... Welcome!");

            PresentMachineOptions(coffeeMachine);
            var input = Console.ReadLine();

            while (true)
            {
                try
                {
                    bool shouldExit = ProcessInput(input, coffeeMachine);
                    if (shouldExit) break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                input = Console.ReadLine();
            }
            Console.ReadKey();
        }

        static void PrintOptions()
        {
            Console.WriteLine("Machine Options: ");
            Console.WriteLine("  Add coin in cents (5, 10, 20, 50, 100, 200) or (C)ancel or E(x)it");
        }

        static bool ProcessInput(string input, CoffeeMachine coffeeMachine)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;

            if (input.ToUpperInvariant() == "C")
            {
                var change = coffeeMachine.CancelTransaction();

                Console.WriteLine($"Change received: {string.Join(" ", change.Select(c => c.ValueInCents))}");
                Console.WriteLine("Transaction cancelled - Goodbye!");
                Console.WriteLine("");
                PresentMachineOptions(coffeeMachine);
                return false;
            }

            if(input.ToUpperInvariant() == "X")
            {
                Console.WriteLine("Coffee machine switched off - Goodbye!");
                return true;
            }

            // Coffee purchase
            if(input.StartsWith("p", StringComparison.OrdinalIgnoreCase))
            {
                var coffee = coffeeMachine.PurchaseCoffee(input[1..]);
                Console.WriteLine($"Purchased {coffee.Name} for {coffee.Price:C}");
            }
            else // Coin added
            {
                coffeeMachine.AddCoin(input);
            }
            ReportBalance(coffeeMachine.GetBalance());

            return false;
        }

        static void PresentMachineOptions(CoffeeMachine coffeeMachine)
        {
            DisplayCoffeeList();
            ReportBalance(coffeeMachine.GetBalance());
            PrintOptions();
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("Please enter an option from the Option Menu...");
        }

        static void ReportBalance(decimal balance) => Console.WriteLine($"Machine Balance: {balance:C}");

        static void DisplayCoffeeList()
        {
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("                             Coffee menu");
            Console.WriteLine("--------------------------------------------------------------------");
            foreach (var coffee in _coffeeMenu)
            {
                Console.WriteLine($"\t\t(P{coffee.Id}) {coffee.Name} - {coffee.Price:C}");
            }
            Console.WriteLine("\t\tEnter P and Coffee Number to purchase.");
        }

    }
}
