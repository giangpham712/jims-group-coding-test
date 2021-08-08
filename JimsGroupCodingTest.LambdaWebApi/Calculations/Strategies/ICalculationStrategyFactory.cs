namespace JimsGroupCodingTest.LambdaWebApi.Calculations.Strategies
{
    public interface ICalculationStrategyFactory
    {
        ICalculationStrategy Create(Operator @operator);
    }
}