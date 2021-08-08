using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JimsGroupCodingTest.ConsoleApp.Apis.RandomNumber;
using JimsGroupCodingTest.ConsoleApp.Options;
using Microsoft.Extensions.Options;

namespace JimsGroupCodingTest.ConsoleApp.Data
{
    public class RandomDataProvider : IDataProvider
    {
        private readonly RandomDataOptions _options;
        private readonly IRandomNumberApi _randomNumberApi;

        public RandomDataProvider(
            IOptions<RandomDataOptions> options,
            IRandomNumberApi randomNumberApi)
        {
            _options = options.Value;
            _randomNumberApi = randomNumberApi;
        }

        public List<decimal> PrepareData()
        {
            var data = Task.Run(async () =>
            {
                var randomNumbers = await this._randomNumberApi.Random(_options.Min, _options.Max, 2);
                return randomNumbers.Select(Convert.ToDecimal).ToList();
            }).GetAwaiter().GetResult();

            return data;
        }
    }
}
