using System;
using System.Collections.Generic;
using System.Text;

namespace JimsGroupCodingTest.ConsoleApp
{
    public class ServiceArgs
    {
        public string Calculation { get; set; }
        public string DataProvider { get; set; }

        public ServiceArgs(string calculation, string dataProvider)
        {
            Calculation = calculation;
            DataProvider = dataProvider;
        }
    }
}
