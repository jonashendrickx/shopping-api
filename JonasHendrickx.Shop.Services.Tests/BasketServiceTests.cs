using System.Threading.Tasks;
using JonasHendrickx.Shop.Contracts;
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
        public async Task CreateAsync_ReturnsId_WhenBasketIsCreated()
        {
            // Arrange
            var id = Guid.NewGuid();
            _basketRepositoryMock.Setup(x => x.CreateAsync()).ReturnsAsync(id);

            // Act

            // Assert
        }
        
        [Test]
        public async Task DeleteAsync_ReturnsCallsRepositoryWithCorrectId_WhenBasketIsDeleted()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            await _sut.DeleteAsync(id);

            // Assert
            _basketRepositoryMock.Verify(x => x.DeleteAsync(It.Is<Guid>(p => p == id)), Times.Once);
        }
    }
}