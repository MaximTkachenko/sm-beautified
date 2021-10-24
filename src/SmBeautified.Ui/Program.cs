using System;
using System.Linq;
using FluentResults;
using SmBeautified.Core;

namespace SmBeautified.Ui
{
    internal class Program
    {
        private static void Main()
        {
            new Executor().Start();
        }
    }

    public sealed class Executor
    {
        private readonly CommandParser _parser = new CommandParser();
        private readonly SodaMachine _machine = new SodaMachine(new []
        {
            new InStock("coke", 20, 5),
            new InStock("sprite", 15, 3),
            new InStock("fanta",15, 3)
        });

        public void Start()
        {
            while (true)
            {
                Console.WriteLine("\n\nAvailable commands:");
                Console.WriteLine("insert (money) - Money put into money slot");
                Console.WriteLine($"order ({_machine.Assortment}) - Order from machines buttons");
                Console.WriteLine($"sms order ({_machine.Assortment}) - Order sent by sms");
                Console.WriteLine("recall - gives money back");
                Console.WriteLine("q - exit");
                Console.WriteLine("-------");
                Console.WriteLine("Inserted money: " + _machine.Money);
                Console.WriteLine("-------\n\n");

                var input = Console.ReadLine();
                if (input == "q") return;

                try
                {
                    var parsedCommand = _parser.Parse(input);

                    switch (parsedCommand)
                    {
                        case InsertMoneyCommand insertCommand:
                            Console.WriteLine("Adding " + insertCommand.Money + " to credit");
                            _machine.InsertMoney(insertCommand.Money);
                            break;
                        case OrderCommand orderCommand:
                            var orderResult = _machine.Order(orderCommand.Soda);
                            if (!orderResult.IsSuccess)
                            {
                                Console.WriteLine(GetError(orderResult));
                                break;
                            }

                            Console.WriteLine($"Giving {orderCommand.Soda} out");
                            if(orderResult.Value > 0) Console.WriteLine($"Giving {orderResult.Value} out in change");
                            break;
                        case OrderBySmsCommand orderBySmsCommand:
                            var orderBySmsResult = _machine.OrderBySms(orderBySmsCommand.Soda);
                            if (!orderBySmsResult.IsSuccess)
                            {
                                Console.WriteLine(GetError(orderBySmsResult));
                                break;
                            }

                            Console.WriteLine($"Giving {orderBySmsCommand.Soda} out");
                            if(orderBySmsResult.Value > 0) Console.WriteLine($"Giving {orderBySmsResult.Value} out in change");
                            break;
                        case RecallCommand _:
                            var recallResult = _machine.Recall();
                            if (!recallResult.IsSuccess)
                            {
                                Console.WriteLine(GetError(recallResult));
                                break;
                            }

                            if (recallResult.Value > 0) Console.WriteLine($"Returning {recallResult.Value} to customer");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static string GetError(Result<int> result) => string.Join(',', result.Errors.Select(x => x.Message));
    }
}
