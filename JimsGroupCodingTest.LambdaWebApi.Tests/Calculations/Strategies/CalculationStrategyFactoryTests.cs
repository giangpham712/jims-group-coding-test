using System;
using JimsGroupCodingTest.LambdaWebApi.Calculations;
using JimsGroupCodingTest.LambdaWebApi.Calculations.Strategies;
using Xunit;

namespace JimsGroupCodingTest.LambdaWebApi.Tests.Calculations.Strategies
{
    public class CalculationStrategyFactoryTests
    {
        [Theory]
        [InlineData(Operator.Addition, typeof(AdditionStrategy))]
        [InlineData(Operator.Subtraction, typeof(SubtractionStrategy))]
        [InlineData(Operator.Multiplication, typeof(MultiplicationStrategy))]
        [InlineData(Operator.Division, typeof(DivisionStrategy))]
        public void Create_ValidOperator_ReturnsCalculationStrategy(Operator @operator, Type expected)
        {
            var factory = new CalculationStrategyFactory();
            var strategy = factory.Create(@operator);

            Assert.Equal(expected, strategy.GetType());
        }

        [Fact]
        public void Create_UnsupportedOperator_ThrowsException()
        {
            var factory = new CalculationStrategyFactory();

            var exception = Assert.Throws<ArgumentException>(() => factory.Create(Operator.Modulo));
            Assert.Equal("Unsupported operator type.", exception.Message);
        }
    }
}
