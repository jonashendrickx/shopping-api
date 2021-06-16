using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using System;
using JonasHendrickx.Shop.Infrastructure.Contracts;

namespace JonasHendrickx.Shop.Services.Tests
{
    public class BasketServiceTests
    {
        private Mock<IBasketRepository> _basketRepositoryMock;
        private BasketService _sut;

        [SetUp]
        public void SetUp()
        {
            _basketRepositoryMock = new Mock<IBasketRepository>();
            _sut = new BasketService(_basketRepositoryMock.Object);
        }
        
        [Test]
        public async Task CreateAsync_ReturnsId_WhenBaskedIsCreated()
        {
            // Arrange
            var id = Guid.NewGuid();
            _basketRepositoryMock.Setup(x => x.CreateAsync()).ReturnsAsync(id);

            // Act
            var actual = await _sut.CreateAsync();

            // Assert
            Assert.AreEqual(id, actual);
        }
    }
}