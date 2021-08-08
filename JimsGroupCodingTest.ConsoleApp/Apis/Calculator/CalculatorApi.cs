using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JimsGroupCodingTest.ConsoleApp.Apis.Calculator
{
    public class CalculatorApi : ICalculatorApi
    {
        private readonly HttpClient _httpClient;

        public CalculatorApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CalculationResponse> Calculate(CalculationRequest request)
        {
            var body = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("calculator/calculate", body);

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CalculationResponse>(responseBody, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
