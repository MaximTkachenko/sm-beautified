using System;

namespace SmBeautified.Ui
{
    public sealed class CommandParser
    {
        public ICommand Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) throw new ArgumentException("Empty command");

            if (input.Equals("recall")) return new RecallCommand();

            if (input.StartsWith("insert "))
            {
                var splitInput = input.Split(' ');
                if (splitInput.Length != 2 || !int.TryParse(splitInput[1], out var parsedMoney) || parsedMoney <= 0)
                {
                    throw new ParserError("Invalid money");
                }

                return new InsertMoneyCommand(parsedMoney);
            }

            if (input.StartsWith("order "))
            {
                var splitInput = input.Split(' ');
                if (splitInput.Length != 2 || string.IsNullOrEmpty(splitInput[1]))
                {
                    throw new ParserError("Invalid soda");
                }

                return new OrderCommand(splitInput[1]);
            }

            if (input.StartsWith("sms order "))
            {
                var splitInput = input.Split(' ');
                if (splitInput.Length != 3 || string.IsNullOrEmpty(splitInput[2]))
                {
                    throw new ParserError("Invalid soda");
                }

                return new OrderBySmsCommand(splitInput[2]);
            }

            throw new ParserError("Invalid command");
        }
    }
}
