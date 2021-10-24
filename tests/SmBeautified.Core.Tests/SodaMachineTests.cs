using FluentAssertions;
using Xunit;

namespace SmBeautified.Core.Tests
{
    public class SodaMachineTests
    {
        [Fact]
        public void InsertMoney_ValidMoney_BalanceIncreased()
        {
            var machine = new SodaMachine(new[]
            {
                new InStock("coke", 20, 5)
            });

            machine.InsertMoney(10);
            machine.InsertMoney(5);

            machine.Money.Should().Be(15);
        }

        [Fact]
        public void Order_EnoughMoneyAndValidSoda_Ok()
        {
            var machine = new SodaMachine(new[]
            {
                new InStock("coke", 20, 5)
            });

            machine.InsertMoney(25);
            var result = machine.Order("coke");

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(5);
            machine.Money.Should().Be(0);
        }

        [Fact]
        public void OrderBySms_EnoughMoneyAndValidSoda_Ok()
        {
            var machine = new SodaMachine(new[]
            {
                new InStock("coke", 20, 5)
            });

            machine.InsertMoney(25);
            var result = machine.OrderBySms("coke");

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(25);
            machine.Money.Should().Be(0);
        }

        [Fact]
        public void Recall_MoneyInserted_Ok()
        {
            var machine = new SodaMachine(new[]
            {
                new InStock("coke", 20, 5)
            });

            machine.InsertMoney(25);
            var result = machine.Recall();

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(25);
            machine.Money.Should().Be(0);
        }
    }
}
