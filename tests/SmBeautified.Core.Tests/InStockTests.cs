using FluentAssertions;
using Xunit;

namespace SmBeautified.Core.Tests
{
    public class InStockTests
    {
        [Fact]
        public void TryGet_EnoughMoneyAndSoda_Success()
        {
            var inStock = new InStock("aa", 10, 5);

            var result = inStock.TryGet(15);

            result.IsSuccess.Should().BeTrue();
            inStock.Count.Should().Be(4);
        }

        [Fact]
        public void TryGet_NotEnoughMoney_Fail()
        {
            var inStock = new InStock("aa", 10, 5);

            var result = inStock.TryGet(5);

            result.IsSuccess.Should().BeFalse();
            inStock.Count.Should().Be(5);
        }

        [Fact]
        public void TryGet_NotSodaLeft_Fail()
        {
            var inStock = new InStock("aa", 10, 1);
            inStock.TryGet(11);

            var result = inStock.TryGet(11);

            result.IsSuccess.Should().BeFalse();
            inStock.Count.Should().Be(0);
        }

        [Fact]
        public void TryGetPaid_EnoughMoneyAndSoda_Success()
        {
            var inStock = new InStock("aa", 10, 1);
            var result = inStock.TryGetPaid();

            result.IsSuccess.Should().BeTrue();
            inStock.Count.Should().Be(0);
        }

        [Fact]
        public void TryGetPaid_NotSodaLeft_Fail()
        {
            var inStock = new InStock("aa", 10, 1);
            inStock.TryGet(11);

            var result = inStock.TryGetPaid();

            result.IsSuccess.Should().BeFalse();
            inStock.Count.Should().Be(0);
        }
    }
}
