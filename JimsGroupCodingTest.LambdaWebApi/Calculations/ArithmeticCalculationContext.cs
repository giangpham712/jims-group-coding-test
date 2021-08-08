using System;
using JimsGroupCodingTest.LambdaWebApi.Calculations.Strategies;

namespace JimsGroupCodingTest.LambdaWebApi.Calculations
{
    public class ArithmeticCalculationContext
    {
        private ICalculationStrategy _calculationStrategy;

        public ArithmeticCalculationContext(ICalculationStrategy calculationStrategy)
        {
            _calculationStrategy = calculationStrategy;
        }

        public void SetCalculationStrategy(ICalculationStrategy strategy)
        {
            _calculationStrategy = strategy;
        }

        public decimal PerformCalculation(decimal a, decimal b)
        {
            try
            {
                return _calculationStrategy.Execute(a, b);
            }
            catch (OverflowException)
            {
                throw new InvalidOperationException("Result for this calculation is too large or too small.");
            }
        }
    }
}
