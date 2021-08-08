using System.Collections.Generic;

namespace JimsGroupCodingTest.ConsoleApp.Apis.Calculator
{
    public class CalculationRequest
    {
        public List<decimal> Inputs { get; set; }
        public string Operator { get; set; }
    }
}
