
using System.Text;
using Core.Entities;
using Core.Services;
using Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Newtonsoft.Json;

namespace Tests
{
    [TestFixture]
    public class ProductService_GetProducts_Tests
    {
        private Mock<IProductRepository> _mockRepo = null!;
        private Mock<IDistributedCache> _mockCache = null!;
        private ProductService _service = null!;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IProductRepository>();
            _mockCache = new Mock<IDistributedCache>();
            _service = new ProductService(_mockRepo.Object, _mockCache.Object);
        }

        [Test]
        public async Task GetProducts_ReturnsFromCache_WhenCacheHasValue()
        {
            // Arrange
            var expected = new List<ProductModel>
            {
                new ProductModel { Id = Guid.NewGuid(), Name = "Smartphone" },
                new ProductModel { Id = Guid.NewGuid(), Name = "T-Shirt" }
            };

            var json = JsonConvert.SerializeObject(expected);
            var bytes = Encoding.UTF8.GetBytes(json);

            // Setup GetAsync to return bytes (so GetStringAsync extension returns the JSON string)
            _mockCache
                .Setup(c => c.GetAsync("list_products", It.IsAny<CancellationToken>()))
                .ReturnsAsync(bytes);

            // Act
            var result = await _service.GetProducts();

            // Assert
            // cache hit -> repository should NOT be called
            _mockRepo.Verify(r => r.GetProducts(), Times.Never);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(expected.Count));
            Assert.That(result[0].Name, Is.EqualTo(expected[0].Name));
            Assert.That(result[1].Name, Is.EqualTo(expected[1].Name));
        }

        [Test]
        public async Task GetProducts_FetchesFromRepo_AndSetsCache_WhenCacheEmpty()
        {
            // Arrange
            // cache miss -> GetAsync returns null
            _mockCache
                .Setup(c => c.GetAsync("list_products", It.IsAny<CancellationToken>()))
                .ReturnsAsync((byte[]?)null);

            var repoList = new List<ProductModel>
            {
                new ProductModel { Id = Guid.NewGuid(), Name = "Coffee Beans" }
            };

            _mockRepo
                .Setup(r => r.GetProducts())
                .ReturnsAsync(repoList);

            // Capture the JSON passed to SetAsync by verifying the byte[] content
            string expectedJson = JsonConvert.SerializeObject(repoList);

            _mockCache
                .Setup(c => c.SetAsync(
                    It.Is<string>(k => k == "list_products"),
                    It.Is<byte[]>(b => Encoding.UTF8.GetString(b) == expectedJson),
                    It.IsAny<DistributedCacheEntryOptions>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Act
            var result = await _service.GetProducts();

            // Assert
            // repository should have been called once
            _mockRepo.Verify(r => r.GetProducts(), Times.Once);

            // SetAsync should have been called with correct JSON bytes
            _mockCache.Verify();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo(repoList[0].Name));
        }
    }
}
