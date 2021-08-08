using System;
using JimsGroupCodingTest.LambdaWebApi.Calculations.Strategies;
using Xunit;

namespace JimsGroupCodingTest.LambdaWebApi.Tests.Calculations.Strategies
{
    public class DivisionStrategyTests
    {
        [Theory]
        [InlineData(1, 2, 0.5)]
        [InlineData(32, -20, -1.6)]
        [InlineData(-1111, 2, -555.5)]
        [InlineData(3000, 15, 200)]
        public void Calculate(decimal x, decimal y, decimal expected)
        {
            var division = new DivisionStrategy();
            var actual = division.Execute(x, y);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(0, 0)]
        public void Calculate_ZeroDivisor_ThrowsException(decimal x, decimal y)
        {
            var division = new DivisionStrategy();

            var exception = Assert.Throws<InvalidOperationException>(() => division.Execute(x, y));
            Assert.Equal("Cannot divide by zero.", exception.Message);
        }
    }
}