using System;
using System.Runtime.CompilerServices;
using FluentResults;

[assembly: InternalsVisibleTo("SmBeautified.Core.Tests")]

namespace SmBeautified.Core
{
    public sealed class InStock
    {
        public InStock(string soda, int price, int count)
        {
            Soda = string.IsNullOrEmpty(soda) ? throw new ArgumentException("Invalid soda") : soda;
            Price = price <= 0 ? throw new ArgumentException("Invalid price") : price;
            Count = count <= 0 ? throw new ArgumentException("Invalid count") : count;
        }

        public string Soda { get; }
        public int Price { get; }
        public int Count { get; private set; }

        internal Result TryGet(int money)
        {
            if (money <= 0) throw new ArgumentException("Invalid money");

            if (Price > money) return Result.Fail($"Need {Price - money} more");

            return TryGetPaid();
        }

        internal Result TryGetPaid()
        {
            if (Count == 0) return Result.Fail($"No {Soda} left");

            Count--;
            return Result.Ok();
        }
    }
}
