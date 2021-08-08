using System.Collections.Generic;
using JimsGroupCodingTest.LambdaWebApi.Calculations;

namespace JimsGroupCodingTest.LambdaWebApi.Application
{
    public class CalculationRequest
    {
        private List<decimal> _inputs;

        public List<decimal> Inputs
        {
            get => _inputs ?? new List<decimal>();
            set => _inputs = value;
        }

        public Operator Operator { get; set; }
    }
}
