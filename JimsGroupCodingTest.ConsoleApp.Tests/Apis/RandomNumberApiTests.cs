using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using JimsGroupCodingTest.ConsoleApp.Apis.RandomNumber;
using Moq;
using Moq.Protected;
using Xunit;

namespace JimsGroupCodingTest.ConsoleApp.Tests.Apis
{
    public class RandomNumberApiTests
    {
        [Fact]
        public async Task Random()
        {
            var min = 10;
            var max = 2000;
            var count = 2;

            var response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(new List<int>() {1200, -5}), Encoding.UTF8,
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

            var api = new RandomNumberApi(httpClient);

            var randomNumbers = await api.Random(min, max, count);

            httpMessageHandlerMock.Protected()
                .Verify(
                    "SendAsync",
                    Times.Once(),
                    ItExpr.Is<HttpRequestMessage>(request => request.Method == HttpMethod.Get && request.RequestUri.AbsoluteUri == $"http://localhost/random?min={min}&max={max}&count={count}"),
                    ItExpr.IsAny<CancellationToken>()
                );

            Assert.Equal(new List<int>() {1200, -5}, randomNumbers);
        }

        [Fact]
        public async Task Random_ErrorStatusCode_ThrowsException()
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

            var api = new RandomNumberApi(httpClient);
            var exception = await Assert.ThrowsAsync<HttpRequestException>(() => api.Random(0, 200, 2));

            Assert.Equal("Response status code does not indicate success: 400 (Error).", exception.Message);
        }

    }
}
