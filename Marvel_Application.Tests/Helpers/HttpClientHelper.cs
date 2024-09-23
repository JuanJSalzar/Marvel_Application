using Moq;
using System.Net;
using Moq.Protected;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Marvel_Application.Tests.Helpers
{
    public static class HttpClientHelper
    {
        public static HttpClient CreateMockHttpClient(HttpStatusCode statusCode, string responseContent)
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = statusCode,
                   Content = new StringContent(responseContent),
               })
               .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new System.Uri("https://gateway.marvel.com/")
            };

            return httpClient;
        }
    }
}
