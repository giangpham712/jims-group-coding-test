using System;
using System.Threading;
using System.Threading.Tasks;
using JimsGroupCodingTest.ConsoleApp.Apis.Calculator;
using JimsGroupCodingTest.ConsoleApp.Data;
using Microsoft.Extensions.Hosting;

namespace JimsGroupCodingTest.ConsoleApp
{
    public class ConsoleHostedService : IHostedService
    {
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly ServiceArgs _serviceArgs;
        private readonly IDataProvider _dataProvider;
        private readonly ICalculatorApi _calculatorApi;

        public ConsoleHostedService(
            IHostApplicationLifetime applicationLifetime,
            ServiceArgs serviceArgs,
            ICalculatorApi calculatorApi, 
            IDataProviderFactory dataProviderFactory)
        {
            _applicationLifetime = applicationLifetime;
            _serviceArgs = serviceArgs;
            _calculatorApi = calculatorApi;
            _dataProvider = dataProviderFactory.Create(serviceArgs.DataProvider);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _applicationLifetime.ApplicationStarted.Register(async () => await OnStarted());

            return Task.CompletedTask;
        }

        private async Task OnStarted()
        {
            try
            {
                var inputs = _dataProvider.PrepareData();
                var calculation = _serviceArgs.Calculation;

                Console.WriteLine($"Inputs: {string.Join(", ", inputs)}");
                Console.WriteLine($"Calculation: {calculation}");

                var calculationResponse = await _calculatorApi.Calculate(new CalculationRequest()
                {
                    Inputs = inputs,
                    Operator = calculation
                });
                
                Console.WriteLine($"Output: {calculationResponse.Output}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            finally
            {
                _applicationLifetime.StopApplication();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
