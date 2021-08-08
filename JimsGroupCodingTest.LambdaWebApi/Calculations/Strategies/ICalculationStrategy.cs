namespace JimsGroupCodingTest.LambdaWebApi.Calculations.Strategies
{
    public interface ICalculationStrategy
    {
        decimal Execute(decimal x, decimal y);
    }
}
