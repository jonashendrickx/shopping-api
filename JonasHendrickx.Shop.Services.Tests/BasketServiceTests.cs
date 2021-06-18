using System.Threading.Tasks;
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
        private Mock<IProductListingRepository> _productListingRepositoryMock;
        private BasketService _sut;

        [SetUp]
        public void SetUp()
        {
            _basketRepositoryMock = new Mock<IBasketRepository>();
            _productListingRepositoryMock = new Mock<IProductListingRepository>();
            _sut = new BasketService(_basketRepositoryMock.Object, _productListingRepositoryMock.Object);
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
                                    Price = 5,
                                    Discounts = new List<Discount>()
                                }
                            },
                            new BasketLineItem
                            {
                                Amount = 3,
                                ProductListing = new ProductListing
                                {
                                    Price = 3,
                                    Discounts = new List<Discount>()
                                }
                            }
                        }
                    });

            // Act
            var actual = await _sut.GetAmountAsync(id);

            // Assert
            Assert.AreEqual(19M, actual);
        }
        
        [Test]
        public async Task GetAmountAsync_ReturnsBasketTotalPrice_WhenBasketHasProductsWithQtyToPctDiscounts()
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
                                    Price = 5,
                                    Discounts = new List<Discount>()
                                }
                            },
                            new BasketLineItem
                            {
                                Amount = 3,
                                ProductListing = new ProductListing
                                {
                                    Price = 3,
                                    Discounts = new List<Discount>
                                    {
                                        new Discount
                                        {
                                            Code = "QTY_TO_PCT",
                                            Rules = "{\"qty\":\"3\",\"pct\":\"0.25\"}"
                                        }
                                    }
                                }
                            }
                        }
                    });

            // Act
            var actual = await _sut.GetAmountAsync(id);

            // Assert
            Assert.AreEqual(16.75M, actual);
        }
        
        [Test]
        public async Task GetAmountAsync_ReturnsBasketTotalPrice_WhenBasketHasProductsWithBuyToFreeQtyDiscounts()
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
                                    Price = 5,
                                    Discounts = new List<Discount>()
                                }
                            },
                            new BasketLineItem
                            {
                                Amount = 3,
                                ProductListing = new ProductListing
                                {
                                    Price = 3,
                                    Discounts = new List<Discount>
                                    {
                                        new Discount
                                        {
                                            Code = "BUY_TO_FREE_QTY",
                                            Rules = "{\"buy_qty\":\"2\",\"free_qty\":\"1\"}"
                                        }
                                    }
                                }
                            }
                        }
                    });

            // Act
            var actual = await _sut.GetAmountAsync(id);

            // Assert
            Assert.AreEqual(16M, actual);
        }

        [Test]
        public async Task GetAmountAsync_ThrowsArgumentException_WhenBasketIsNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            _basketRepositoryMock
                .Setup(x => x.GetAsync(It.Is<Guid>(p => p == id)))
                .ReturnsAsync((Basket)null);

            // Act
            try
            {
                await _sut.GetAmountAsync(id);
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("BASKET_NOT_FOUND", e.Message);
            }

            // Assert
        }

        
        [Test]
        public async Task AddProductListingAsync_ThrowsArgumentException_WhenBasketDoesNotExist()
        {
            // Arrange
            var basketId = Guid.NewGuid();
            var productListingId = Guid.NewGuid();
            
            // Act
            try
            {
                await _sut.AddProductListingAsync(basketId, productListingId, 1);
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("basketId", e.ParamName);
            }

            // Assert
            _basketRepositoryMock.Verify(x => x.GetAsync(It.Is<Guid>(p => p == basketId)), Times.Once);
        }
        
        [Test]
        public async Task AddProductListingAsync_ThrowsArgumentException_WhenProductLineItemDoesNotExist()
        {
            // Arrange
            var basketId = Guid.NewGuid();
            var productListingId = Guid.NewGuid();
            _basketRepositoryMock.Setup(x => x.GetAsync(It.Is<Guid>(p => p == basketId)))
                .ReturnsAsync(new Basket());
            
            // Act
            try
            {
                await _sut.AddProductListingAsync(basketId, productListingId, 1);
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("productListingId", e.ParamName);
            }

            // Assert
            _basketRepositoryMock.Verify(x => x.GetAsync(It.Is<Guid>(p => p == basketId)), Times.Once);
            _productListingRepositoryMock.Verify(x => x.GetAsync(It.Is<Guid>(p => p == productListingId)), Times.Once);
        }
        
        [Test]
        public async Task AddProductListingAsync_ReturnsBasketLineItemId_WhenIsInserted()
        {
            // Arrange
            var basketId = Guid.NewGuid();
            var productListingId = Guid.NewGuid();
            var expected = Guid.NewGuid();
            _basketRepositoryMock.Setup(x => x.GetAsync(It.Is<Guid>(p => p == basketId)))
                .ReturnsAsync(new Basket());
            _productListingRepositoryMock.Setup(x => x.GetAsync(It.Is<Guid>(p => p == productListingId)))
                .ReturnsAsync(new ProductListing());
            _basketRepositoryMock.Setup(x => x.AddProductLineItemAsync(It.Is<Guid>(p => p == basketId), It.Is<Guid>(p => p == productListingId), It.Is<uint>(p => p == 1)))
                .ReturnsAsync(expected);
            
            // Act
            var actual = await _sut.AddProductListingAsync(basketId, productListingId, 1);

            // Assert
            _basketRepositoryMock.Verify(x => x.GetAsync(It.Is<Guid>(p => p == basketId)), Times.Once);
            _productListingRepositoryMock.Verify(x => x.GetAsync(It.Is<Guid>(p => p == productListingId)), Times.Once);
            Assert.AreEqual(expected, actual);
        }
    }
}