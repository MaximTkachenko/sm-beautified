using System;
using FluentAssertions;
using Xunit;

namespace SmBeautified.Ui.Tests
{
    public class CommandParserTests
    {
        [Fact]
        public void Parse_ValidInsertCommand_Ok()
        {
            var command = new CommandParser().Parse("insert 10");

            command.GetType().Should().Be(typeof(InsertMoneyCommand));
            ((InsertMoneyCommand) command).Money.Should().Be(10);
        }

        [Fact]
        public void Parse_ValidOrderCommand_Ok()
        {
            var command = new CommandParser().Parse("order coke");

            command.GetType().Should().Be(typeof(OrderCommand));
            ((OrderCommand)command).Soda.Should().Be("coke");
        }

        [Fact]
        public void Parse_ValidOrderBySmsCommand_Ok()
        {
            var command = new CommandParser().Parse("sms order coke");

            command.GetType().Should().Be(typeof(OrderBySmsCommand));
            ((OrderBySmsCommand)command).Soda.Should().Be("coke");
        }

        [Fact]
        public void Parse_ValidRecallCommand_Ok()
        {
            var command = new CommandParser().Parse("recall");

            command.GetType().Should().Be(typeof(RecallCommand));
        }

        [Theory]
        [InlineData("aa")]
        [InlineData("ins")]
        [InlineData("insert")]
        [InlineData("insert -1")]
        [InlineData("insert 0")]
        [InlineData("insert aa")]
        [InlineData("insert aa10")]
        [InlineData("insert  10")]
        [InlineData("ord")]
        [InlineData("order  coke")]
        [InlineData("order coke ")]
        [InlineData("order coke aa")]
        [InlineData("sms ord")]
        [InlineData("sms order  coke")]
        [InlineData("sms order coke ")]
        [InlineData("sms order coke aa")]
        [InlineData("rec")]
        [InlineData("recall ")]
        [InlineData("recall coke")]
        [InlineData("recall coke aa")]
        public void Parse_InvalidCommand_Error(string input)
        {
            Action action = () => new CommandParser().Parse(input);

            action.Should().Throw<ParserError>();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Parse_EmptyCommand_Error(string input)
        {
            Action action = () => new CommandParser().Parse(input);

            action.Should().Throw<ArgumentException>();
        }
    }
}
