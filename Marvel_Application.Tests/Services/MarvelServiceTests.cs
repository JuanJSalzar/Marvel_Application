using Marvel_Application.IServices;
using Marvel_Application.Models;
using Marvel_Application.Services;
using Moq;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Marvel_Application.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Marvel_Application.Tests.Services
{
    [TestClass]
    public class MarvelServiceTests
    {
        private Mock<IMarvelConfig> _marvelConfigMock;
        private HttpClient _httpClient;
        private MarvelService _marvelService;

        [TestInitialize]
        public void Setup()
        {
            _marvelConfigMock = new Mock<IMarvelConfig>();
            _marvelConfigMock.Setup(config => config.PublicKey).Returns("test_public_key");
            _marvelConfigMock.Setup(config => config.PrivateKey).Returns("test_private_key");

            var apiResponse = new MarvelApiResponse
            {
                Data = new DataContainer
                {
                    Results = new List<Character>
                    {
                        new Character
                        {
                            Id = 1,
                            Name = "Spider-Man",
                            Description = "Friendly neighborhood Spider-Man",
                            Thumbnail = new Thumbnail
                            {
                                Path = "http://example.com/spiderman",
                                Extension = "jpg"
                            }
                        },
                        new Character
                        {
                            Id = 2,
                            Name = "Iron Man",
                            Description = "Genius billionaire playboy philanthropist",
                            Thumbnail = new Thumbnail
                            {
                                Path = "http://example.com/ironman",
                                Extension = "jpg"
                            }
                        }
                    }
                }
            };

            var jsonResponse = JsonConvert.SerializeObject(apiResponse);
            _httpClient = HttpClientHelper.CreateMockHttpClient(HttpStatusCode.OK, jsonResponse);

            _marvelService = new MarvelService(_marvelConfigMock.Object, _httpClient);
        }

        [TestMethod]
        public async Task GetCharactersAsync_ReturnsCharacters_WhenApiResponseIsValid()
        {
            // Arrange
            var limit = 2;
            var offset = 0;

            // Act
            var result = await _marvelService.GetCharactersAsync(limit, offset);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Spider-Man", result[0].Name);
            Assert.AreEqual("Iron Man", result[1].Name);
        }
    }
}
