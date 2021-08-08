using JimsGroupCodingTest.LambdaWebApi.Calculations.Strategies;
using Xunit;

namespace JimsGroupCodingTest.LambdaWebApi.Tests.Calculations.Strategies
{
    public class SubtractionStrategyTests
    {
        [Theory]
        [InlineData(1, 2, -1)]
        [InlineData(-4, -6, 2)]
        [InlineData(-122.55, 20.5, -143.05)]
        [InlineData(-2, 2, -4)]
        public void Calculate(decimal x, decimal y, decimal expected)
        {
            var subtraction = new SubtractionStrategy();
            var actual = subtraction.Execute(x, y);

            Assert.Equal(expected, actual);
        }
    }
}