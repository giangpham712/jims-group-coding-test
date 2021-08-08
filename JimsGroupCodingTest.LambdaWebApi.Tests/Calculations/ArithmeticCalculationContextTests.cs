using System;
using JimsGroupCodingTest.LambdaWebApi.Calculations;
using JimsGroupCodingTest.LambdaWebApi.Calculations.Strategies;
using Moq;
using Xunit;

namespace JimsGroupCodingTest.LambdaWebApi.Tests.Calculations
{
    public class ArithmeticCalculationContextTests
    {
        [Theory]
        [InlineData(1, 2, 0.5)]
        [InlineData(-1, 122, -122)]
        [InlineData(1000, 0.5, 500)]
        [InlineData(-1111, 2, -555.5)]
        [InlineData(1, 2, 3)]
        [InlineData(-4, -6, -10)]
        public void PerformCalculation(decimal x, decimal y, decimal expected)
        {
            var calculationStrategyMock = new Mock<ICalculationStrategy>();
            calculationStrategyMock.Setup(p => p.Execute(x, y)).Returns(expected);

            var calculationContext = new ArithmeticCalculationContext(calculationStrategyMock.Object);
            var actual = calculationContext.PerformCalculation(x, y);

            calculationStrategyMock.Verify(mock => mock.Execute(x, y), Times.Once);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PerformCalculation_Overflow_ThrowsException()
        {
            var calculationStrategyMock = new Mock<ICalculationStrategy>();
            calculationStrategyMock
                .Setup(p => p.Execute(It.IsAny<decimal>(), It.IsAny<decimal>()))
                .Throws<OverflowException>();

            var calculationContext = new ArithmeticCalculationContext(calculationStrategyMock.Object);

            var exception = Assert.Throws<InvalidOperationException>(() => calculationContext.PerformCalculation(1, 4));
            Assert.Equal("Result for this calculation is too large or too small.", exception.Message);
        }
    }
}
