using JimsGroupCodingTest.LambdaWebApi.Calculations.Strategies;
using Xunit;

namespace JimsGroupCodingTest.LambdaWebApi.Tests.Calculations.Strategies
{
    public class AdditionStrategyTests
    {
        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(-4, -6, -10)]
        [InlineData(-2.5, 20.5, 18)]
        [InlineData(-2, 2, 0)]
        public void Calculate(decimal x, decimal y, decimal expected)
        {
            var addition = new AdditionStrategy();
            var actual = addition.Execute(x, y);

            Assert.Equal(expected, actual);
        }
    }
}
