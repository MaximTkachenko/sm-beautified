using FluentAssertions;
using Xunit;

namespace SmBeautified.Core.Tests
{
    public class SodaMachineTests
    {
        [Fact]
        public void InsertMoney_ValidMoney_IncreasedBalance()
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
        public void Order_EnoughMoneyAndValidSoda_GotSoda()
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
        public void Order_NotEnoughMoneyAndValidSoda_Fail()
        {
            var machine = new SodaMachine(new[]
            {
                new InStock("coke", 20, 5)
            });

            machine.InsertMoney(15);
            var result = machine.Order("coke");

            result.IsSuccess.Should().BeFalse();
            machine.Money.Should().Be(15);
        }

        [Fact]
        public void OrderBySms_EnoughMoneyAndValidSoda_GotSoda()
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
        public void OrderBySms_NoMoney_GotSoda()
        {
            var machine = new SodaMachine(new[]
            {
                new InStock("coke", 20, 5)
            });

            var result = machine.OrderBySms("coke");

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(0);
            machine.Money.Should().Be(0);
        }

        [Fact]
        public void Recall_MoneyInserted_ReturnedMoney()
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

        [Fact]
        public void Recall_NoMoney_Fail()
        {
            var machine = new SodaMachine(new[]
            {
                new InStock("coke", 20, 5)
            });

            var result = machine.Recall();

            result.IsSuccess.Should().BeFalse();
            machine.Money.Should().Be(0);
        }
    }
}
