using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using JimsGroupCodingTest.ConsoleApp.Apis.Calculator;
using JimsGroupCodingTest.ConsoleApp.Apis.RandomNumber;
using Moq;
using Moq.Protected;
using Xunit;

namespace JimsGroupCodingTest.ConsoleApp.Tests.Apis
{
    public class CalculatorApiTests
    {
        [Fact]
        public async Task Calculate()
        {
            var response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(new CalculationResponse() { Output = 20.5M }), Encoding.UTF8,
                    "application/json")
            };

            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var httpClient = new HttpClient(httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("http://localhost")
            };

            var api = new CalculatorApi(httpClient);

            var calculationResponse = await api.Calculate(new CalculationRequest());

            httpMessageHandlerMock.Protected()
                .Verify(
                    "SendAsync",
                    Times.Once(),
                    ItExpr.Is<HttpRequestMessage>(request => request.Method == HttpMethod.Post && request.RequestUri.AbsoluteUri == "http://localhost/calculator/calculate"),
                    ItExpr.IsAny<CancellationToken>()
                );
            Assert.Equal(20.5M, calculationResponse.Output);
        }

        [Fact]
        public async Task Calculate_ErrorStatusCode_ThrowsException()
        {
            var response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                ReasonPhrase = "Error"
            };

            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var httpClient = new HttpClient(httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("http://localhost")
            };

            var api = new CalculatorApi(httpClient);
            var exception = await Assert.ThrowsAsync<HttpRequestException>(() => api.Calculate(new CalculationRequest()));

            
            Assert.Equal("Response status code does not indicate success: 400 (Error).", exception.Message);
        }
    }
}
