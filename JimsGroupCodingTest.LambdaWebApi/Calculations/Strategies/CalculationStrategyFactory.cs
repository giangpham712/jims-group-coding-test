using System;

namespace JimsGroupCodingTest.LambdaWebApi.Calculations.Strategies
{
    public class CalculationStrategyFactory : ICalculationStrategyFactory
    {

        public ICalculationStrategy Create(Operator @operator)
        {
            switch (@operator)
            {
                case Operator.Addition:
                    return new AdditionStrategy();
                case Operator.Subtraction:
                    return new SubtractionStrategy();
                case Operator.Multiplication:
                    return new MultiplicationStrategy();
                case Operator.Division:
                    return new DivisionStrategy();
            }

            throw new ArgumentException("Unsupported operator type.");
        }
    }
}
