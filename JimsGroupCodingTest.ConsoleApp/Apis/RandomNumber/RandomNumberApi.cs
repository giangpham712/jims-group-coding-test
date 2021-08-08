using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace JimsGroupCodingTest.ConsoleApp.Apis.RandomNumber
{
    public class RandomNumberApi : IRandomNumberApi
    {
        private readonly HttpClient _httpClient;

        public RandomNumberApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<int>> Random(int min, int max, int count)
        {
            var response = await _httpClient.GetAsync($"random?min={min}&max={max}&count={count}");

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<int>>(responseBody);
        }
    }
}
