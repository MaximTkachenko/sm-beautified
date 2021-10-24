using System;

namespace SmBeautified.Ui
{
    public interface ICommand
    { }

    public sealed class InsertMoneyCommand : ICommand
    {
        public InsertMoneyCommand(int money)
        {
            if (money <= 0) throw new ArgumentException("Invalid money");
            Money = money;
        }

        public int Money { get; }
    }

    public sealed class OrderCommand : ICommand
    {
        public OrderCommand(string soda)
        {
            Soda = string.IsNullOrWhiteSpace(soda) 
                ? throw new ArgumentException("Invalid soda")
                : soda;
        }

        public string Soda { get; }
    }

    public sealed class OrderBySmsCommand : ICommand
    {
        public OrderBySmsCommand(string soda)
        {
            Soda = string.IsNullOrWhiteSpace(soda)
                ? throw new ArgumentException("Invalid soda")
                : soda;
        }

        public string Soda { get; }
    }

    public sealed class RecallCommand : ICommand
    { }
}
