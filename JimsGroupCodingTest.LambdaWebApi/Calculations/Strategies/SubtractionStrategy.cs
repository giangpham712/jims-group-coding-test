namespace JimsGroupCodingTest.LambdaWebApi.Calculations.Strategies
{
    public class SubtractionStrategy : ICalculationStrategy
    {
        public decimal Execute(decimal x, decimal y)
        {
            return x - y;
        }
    }
}