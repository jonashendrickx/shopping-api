using System.Threading.Tasks;
using JonasHendrickx.Shop.Contracts;
using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using JonasHendrickx.Shop.Infrastructure.Contracts;
using JonasHendrickx.Shop.Models.Entities;

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
            var actual = await _sut.CreateAsync();

            // Assert
            Assert.AreEqual(id, actual);
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
        
        [Test]
        public async Task GetAmountAsync_ReturnsBasketTotalPrice_WhenBasketHasProducts()
        {
            // Arrange
            var id = Guid.NewGuid();
            _basketRepositoryMock
                .Setup(x => x.GetAsync(It.Is<Guid>(p => p == id)))
                .ReturnsAsync(
                    new Basket
                    {
                        Id = id,
                        LineItems = new List<BasketLineItem>
                        {
                            new BasketLineItem
                            {
                                Amount = 2,
                                ProductListing = new ProductListing
                                {
                                    Price = 5
                                }
                            },
                            new BasketLineItem
                            {
                                Amount = 3,
                                ProductListing = new ProductListing
                                {
                                    Price = 3
                                }
                            }
                        }
                    });

            // Act
            var actual = await _sut.GetAmountAsync(id);

            // Assert
            Assert.AreEqual(19M, actual);
        }
    }
}