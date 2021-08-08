using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace JimsGroupCodingTest.ConsoleApp.Data
{
    public class DataProviderFactory : IDataProviderFactory
    {
        private readonly Dictionary<string, Type> _dataProviders;
        private readonly IServiceProvider _serviceProvider;

        public DataProviderFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _dataProviders = new Dictionary<string, Type>();
        }

        public void AddDataProvider(string type, Type dataProviderType)
        {
            _dataProviders.Add(type, dataProviderType);
        }

        public IDataProvider Create(string type)
        {
            if (_dataProviders.TryGetValue(type, out Type dataProviderType) &&
                _serviceProvider.GetService(dataProviderType) is IDataProvider dataProvider)
            {
                return dataProvider;
            }

            throw new ArgumentException("Invalid data provider type");
        }
    }
}
