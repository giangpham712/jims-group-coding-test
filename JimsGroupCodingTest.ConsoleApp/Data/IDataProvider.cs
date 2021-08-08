using System.Collections.Generic;

namespace JimsGroupCodingTest.ConsoleApp.Data
{
    public interface IDataProvider
    {
        List<decimal> PrepareData();
    }
}
