using System;
using System.Collections.Generic;

namespace JimsGroupCodingTest.ConsoleApp.Data
{
    public class HardcodedDataProvider : IDataProvider
    {
        public List<decimal> PrepareData()
        {
            return new List<decimal>()
            {
                200,
                50.5M
            };
        }
    }
}
