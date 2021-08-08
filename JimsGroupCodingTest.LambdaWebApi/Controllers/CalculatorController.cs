using JimsGroupCodingTest.LambdaWebApi.Application;
using Microsoft.AspNetCore.Mvc;

namespace JimsGroupCodingTest.LambdaWebApi.Controllers
{
    [Route("api/[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculatorService _calculatorService;

        public CalculatorController(ICalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }

        [HttpPost]
        [Route("Calculate")]
        public CalculationResponse Post([FromBody] CalculationRequest request)
        {
            return _calculatorService.Calculate(request);
        }
    }
}
