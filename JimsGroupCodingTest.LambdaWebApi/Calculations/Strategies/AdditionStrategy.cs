namespace JimsGroupCodingTest.LambdaWebApi.Calculations.Strategies
{
    public class AdditionStrategy : ICalculationStrategy
    {
        public decimal Execute(decimal x, decimal y)
        {
            return x + y;
        }
    }
}
