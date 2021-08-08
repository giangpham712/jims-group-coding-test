using System;
using JimsGroupCodingTest.LambdaWebApi.Calculations;
using JimsGroupCodingTest.LambdaWebApi.Calculations.Strategies;

namespace JimsGroupCodingTest.LambdaWebApi.Application
{
    public class CalculatorService : ICalculatorService
    {
        private readonly ICalculationStrategyFactory _calculationStrategyFactory;

        public CalculatorService(ICalculationStrategyFactory calculationStrategyFactory)
        {
            _calculationStrategyFactory = calculationStrategyFactory;
        }

        public CalculationResponse Calculate(CalculationRequest request)
        {
            if (request == null)
            {
                throw new ArgumentException("Invalid calculation request.");
            }

            if (request.Inputs.Count != 2)
            {
                throw new ArgumentException("Input must contain two numbers.");
            }

            var strategy = _calculationStrategyFactory.Create(request.Operator);
            var calculationContext = new ArithmeticCalculationContext(strategy);

            return new CalculationResponse()
            {
                Output = calculationContext.PerformCalculation(request.Inputs[0], request.Inputs[1])
            };
        }
    }

    public interface ICalculatorService
    {
        CalculationResponse Calculate(CalculationRequest request);
    }
}
