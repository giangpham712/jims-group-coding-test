using System;

namespace JimsGroupCodingTest.LambdaWebApi.Calculations.Strategies
{
    public class DivisionStrategy : ICalculationStrategy
    {
        public decimal Execute(decimal x, decimal y)
        {
            if (y == 0)
            {
                throw new InvalidOperationException("Cannot divide by zero.");
            }

            return x / y;
        }
    }
}