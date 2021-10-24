using System;
using System.Collections.Generic;
using System.Linq;
using FluentResults;

namespace SmBeautified.Core
{
    public sealed class SodaMachine
    {
        private readonly Dictionary<string, InStock> _stock;

        public SodaMachine(InStock[] stock)
        {
            if(stock == null) throw new ArgumentException("Null in stock");

            _stock = stock.ToDictionary(x => x.Soda, x => x);
        }

        public int Money { get; private set; }

        public void InsertMoney(int money)
        {
            if (money <= 0) throw new ArgumentException("Money is less than or equal to zero");

            Money += money;
        }

        /// <summary>
        /// Returns change
        /// </summary>
        public Result<int> Order(string soda)
        {
            if (string.IsNullOrEmpty(soda)) throw new ArgumentException("Invalid soda");

            if (!_stock.TryGetValue(soda, out var sodaInStock)) return Result.Fail($"No {soda} left");

            var result = sodaInStock.TryGet(Money);
            if (result.IsSuccess)
            {
                var change = Money - sodaInStock.Price;
                Money = 0;
                return Result.Ok(change);
            }

            return result;
        }

        /// <summary>
        /// Returns change
        /// </summary>
        public Result<int> OrderBySms(string soda)
        {
            if (string.IsNullOrEmpty(soda)) throw new ArgumentException("Invalid soda");

            if (!_stock.TryGetValue(soda, out var sodaInStock)) return Result.Fail($"No {soda} left");

            var result = sodaInStock.TryGetPaid();
            if (result.IsSuccess)
            {
                var change = Money;
                Money = 0;
                return Result.Ok(change);
            }
            
            return result;
        }

        /// <summary>
        /// Returns money to return
        /// </summary>
        public Result<int> Recall()
        {
            if (Money == 0) return Result.Fail("Nothing to recall");

            var change = Money;
            Money = 0;
            return Result.Ok(change);
        }
    }
}
