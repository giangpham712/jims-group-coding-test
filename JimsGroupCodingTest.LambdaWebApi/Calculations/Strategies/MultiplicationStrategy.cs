namespace JimsGroupCodingTest.LambdaWebApi.Calculations.Strategies
{
    public class MultiplicationStrategy : ICalculationStrategy
    {
        public decimal Execute(decimal x, decimal y)
        {
            return x * y;
        }
    }
}