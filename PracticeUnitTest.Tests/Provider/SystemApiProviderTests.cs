using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using PracticeUnitTest.Provider.Entity;
using PracticeUnitTest.Provider.Implementations;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PracticeUnitTest.Tests.Provider
{
    public class SystemApiProviderTests
    {
        private const string SystemApiUrl = "api/sys_api";
        private const string BaseAddress = "https://github.com";

        private readonly Mock<HttpMessageHandler> _mockHandler;
        private readonly SystemApiProvider _systemApiProvider;

        public SystemApiProviderTests()
        {
            // Setup HttpClient
            _mockHandler = new Mock<HttpMessageHandler>();

            // Use real http client with mocked handler here
            var httpClient = new HttpClient(_mockHandler.Object)
            {
                BaseAddress = new Uri("https://github.com")
            };

            var httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory
                .Setup(x => x.CreateClient(It.IsAny<string>()))
                .Returns(httpClient);

            // Setup Logger
            var logger = new Mock<ILogger<SystemApiProvider>>();

            // Initialize
            _systemApiProvider = new SystemApiProvider(httpClientFactory.Object, logger.Object);
        }

        [Fact]
        public async Task GetSysInfoAsync_WhenCalled_InvokeTimeOnceGetAsync()
        {
            // Arrange
            _mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(new SystemApiResponse())),
                });

            var expectedRequestUri = new Uri($"{BaseAddress}/{SystemApiUrl}");

            // Act
            await _systemApiProvider.GetSysInfoAsync(It.IsAny<SystemApiRequest>());

            // Assert
            // We expected a single external request
            // We expected HTTP GET
            // We expected request uri is correct
            _mockHandler.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get
                    && req.RequestUri == expectedRequestUri),
                ItExpr.IsAny<CancellationToken>()
            );
        }
    }
}