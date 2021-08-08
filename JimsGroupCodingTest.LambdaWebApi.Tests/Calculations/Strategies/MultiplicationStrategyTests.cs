using JimsGroupCodingTest.LambdaWebApi.Calculations.Strategies;
using Xunit;

namespace JimsGroupCodingTest.LambdaWebApi.Tests.Calculations.Strategies
{
    public class MultiplicationStrategyTests
    {
        [Theory]
        [InlineData(1, 2, 2)]
        [InlineData(100.0, 2.2, 220.0)]
        [InlineData(-1, 122, -122)]
        [InlineData(1000, 0.5, 500)]
        public void Calculate(decimal x, decimal y, decimal expected)
        {
            var multiplication = new MultiplicationStrategy();
            var actual = multiplication.Execute(x, y);
            
            Assert.Equal(expected, actual);
        }
    }
}