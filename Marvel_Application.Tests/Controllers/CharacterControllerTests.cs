using Marvel_Application.Controllers;
using Marvel_Application.IServices;
using Marvel_Application.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Marvel_Application.Tests.Controllers
{
    [TestClass]
    public class CharacterControllerTests
    {
        private Mock<IMarvelService> _marvelServiceMock;
        private ICharactersController _characterController;

        [TestInitialize]
        public void Setup()
        {
            _marvelServiceMock = new Mock<IMarvelService>();
            _characterController = new CharactersController(_marvelServiceMock.Object);
        }

        [TestMethod]
        public async Task GetSelectedCharactersAsync_ReturnsSelectedCharacters()
        {
            // Arrange
            int totalToFetch = 200;
            int selectCount = 50;
            int batchSize = 100;
            var charactersBatch1 = new List<Character>();
            for (int i = 1; i <= 100; i++)
            {
                charactersBatch1.Add(new Character
                {
                    Id = i,
                    Name = $"Character {i}",
                    Description = $"Description {i}",
                    Thumbnail = new Thumbnail { Path = "http://example.com/image", Extension = "jpg" }
                });
            }

            var charactersBatch2 = new List<Character>();
            for (int i = 101; i <= 200; i++)
            {
                charactersBatch2.Add(new Character
                {
                    Id = i,
                    Name = $"Character {i}",
                    Description = $"Description {i}",
                    Thumbnail = new Thumbnail { Path = "http://example.com/image", Extension = "jpg" }
                });
            }

            _marvelServiceMock.Setup(s => s.GetCharactersAsync(batchSize, 0))
                .ReturnsAsync(charactersBatch1);
            _marvelServiceMock.Setup(s => s.GetCharactersAsync(batchSize, 100))
                .ReturnsAsync(charactersBatch2);

            // Act
            var result = await _characterController.GetSelectedCharactersAsync(totalToFetch, selectCount);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(selectCount, result.Count);
            Assert.IsTrue(result.All(c => c.Thumbnail != null && !c.Thumbnail.Path.Contains("image_not_available")));
        }
    }
}
