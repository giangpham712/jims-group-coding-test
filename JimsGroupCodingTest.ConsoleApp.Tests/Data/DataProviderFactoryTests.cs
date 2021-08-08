using System;
using System.Collections.Generic;
using System.Text;
using JimsGroupCodingTest.ConsoleApp.Data;
using Moq;
using Xunit;

namespace JimsGroupCodingTest.ConsoleApp.Tests.Data
{
    public class DataProviderFactoryTests
    {
        [Fact]
        public void Create_ValidType_ReturnsDataProvider()
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(x => x.GetService(typeof(HardcodedDataProvider)))
                .Returns(new HardcodedDataProvider());

            var factory = new DataProviderFactory(serviceProviderMock.Object);
            factory.AddDataProvider("Hardcoded", typeof(HardcodedDataProvider));

            var dataProvider = factory.Create("Hardcoded");

            Assert.Equal(typeof(HardcodedDataProvider), dataProvider.GetType());
        }

        [Fact]
        public void Create_InvalidType_ThrowsException()
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            var factory = new DataProviderFactory(serviceProviderMock.Object);

            var exception = Assert.Throws<ArgumentException>(() => factory.Create("Hardcoded"));
            Assert.Equal("Invalid data provider type", exception.Message);
        }
    }
}
