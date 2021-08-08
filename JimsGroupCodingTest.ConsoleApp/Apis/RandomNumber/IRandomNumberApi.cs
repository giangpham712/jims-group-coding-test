using System.Collections.Generic;
using System.Threading.Tasks;

namespace JimsGroupCodingTest.ConsoleApp.Apis.RandomNumber
{
    public interface IRandomNumberApi
    {
        Task<List<int>> Random(int min, int max, int count);
    }
}