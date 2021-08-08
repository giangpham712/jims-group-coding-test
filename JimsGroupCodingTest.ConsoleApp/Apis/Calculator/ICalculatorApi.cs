using System.Threading.Tasks;

namespace JimsGroupCodingTest.ConsoleApp.Apis.Calculator
{
    public interface ICalculatorApi
    {
        Task<CalculationResponse> Calculate(CalculationRequest request);
    }
}