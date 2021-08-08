using System;
using System.IO;
using System.Threading.Tasks;
using JimsGroupCodingTest.ConsoleApp.Apis.Calculator;
using JimsGroupCodingTest.ConsoleApp.Apis.RandomNumber;
using JimsGroupCodingTest.ConsoleApp.Data;
using JimsGroupCodingTest.ConsoleApp.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JimsGroupCodingTest.ConsoleApp
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            var calculatorApiOptions = configuration.GetSection("CalculatorApi").Get<CalculatorApiOptions>();
            var randomNumberApiOptions = configuration.GetValue<string>("RandomData:ApiBaseUrl");

            if (args.Length < 2)
            {
                throw new ArgumentException("Missing service arguments.");
            }

            await Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddTransient<ServiceArgs>((serviceProvider) =>
                    {
                        var calculation = args[0];
                        var dataProvider = args[1];

                        return new ServiceArgs(calculation, dataProvider);
                    });

                    services.AddHostedService<ConsoleHostedService>();
                    services.AddHttpClient<ICalculatorApi, CalculatorApi>(client =>
                    {
                        client.BaseAddress = new Uri(calculatorApiOptions.BaseUrl);
                    });

                    services.AddHttpClient<IRandomNumberApi, RandomNumberApi>(client =>
                    {
                        client.BaseAddress = new Uri(configuration.GetValue<string>("RandomData:ApiBaseUrl"));
                    });

                    services.Configure<RandomDataOptions>(configuration.GetSection("RandomData"));

                    services.AddTransient<HardcodedDataProvider>();
                    services.AddTransient<RandomDataProvider>();
                    services.AddTransient<IDataProviderFactory>(serviceProvider =>
                    {
                        var dataProviderFactory = new DataProviderFactory(serviceProvider);
                        dataProviderFactory.AddDataProvider("Hardcoded", typeof(HardcodedDataProvider));
                        dataProviderFactory.AddDataProvider("Random", typeof(RandomDataProvider));
                        return dataProviderFactory;
                    });

                })
                .RunConsoleAsync();
        }
    }
}
