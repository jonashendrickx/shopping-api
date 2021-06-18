using System.Threading.Tasks;
using JonasHendrickx.Shop.Contracts;
using NUnit.Framework;
using Moq;
using System;
using JonasHendrickx.Shop.Api.Contracts.Basket;
using JonasHendrickx.Shop.Api.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace JonasHendrickx.Shop.Api.Tests.Controllers
{
    public class BasketControllerTests
    {
        private Mock<IBasketService> _basketServiceMock;
        private Mock<ILogger<BasketController>> _loggerMock;
        private BasketController _sut;
        
        [SetUp]
        public void SetUp()
        {
            _basketServiceMock = new Mock<IBasketService>();
            _loggerMock = new Mock<ILogger<BasketController>>();
            _sut = new BasketController(_basketServiceMock.Object, _loggerMock.Object);
        }
        
        [Test]
        public async Task Create_ReturnsId_WhenBasketIsCreated()
        {
            // Arrange
            var id = Guid.NewGuid();
            _basketServiceMock.Setup(x => x.CreateAsync()).ReturnsAsync(id);

            // Act
            var response = await _sut.Create();

            // Assert
            Assert.AreEqual(typeof(OkObjectResult), response.GetType());
            var result = (OkObjectResult)response;
            Assert.AreEqual(id, (Guid)result.Value);
        }
        
        [Test]
        public async Task Delete_ReturnsNoContent_WhenBasketIsDeleted()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var response = await _sut.Delete(id);

            // Assert
            Assert.AreEqual(typeof(NoContentResult), response.GetType());
            _basketServiceMock.Verify(x => x.DeleteAsync(It.Is<Guid>(p => p == id)), Times.Once);
        }
        
        [Test]
        public async Task GetAmount_ReturnOk_WhenBasketTotalAmountIsCalculated()
        {
            // Arrange
            var id = Guid.NewGuid();
            _basketServiceMock.Setup(x => x.GetAmountAsync(It.Is<Guid>(p => p == id))).ReturnsAsync(19M);

            // Act
            var response = await _sut.GetAmount(id);

            // Assert
            Assert.AreEqual(typeof(OkObjectResult), response.GetType());
            var actual = (OkObjectResult)response;
            Assert.AreEqual(19M, (decimal)actual.Value);

            _basketServiceMock.Verify(x => x.GetAmountAsync(It.Is<Guid>(p => p == id)), Times.Once);
        }
        
        [Test]
        public async Task GetAmount_ReturnBadRequest_WhenBasketNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            _basketServiceMock.Setup(x => x.GetAmountAsync(It.Is<Guid>(p => p == id))).Throws<ArgumentException>();

            // Act
            var response = await _sut.GetAmount(id);

            // Assert
            Assert.AreEqual(typeof(BadRequestObjectResult), response.GetType());
            _basketServiceMock.Verify(x => x.GetAmountAsync(It.Is<Guid>(p => p == id)), Times.Once);
        }
        
        [Test]
        public async Task AddProductListing_ReturnOk_WhenInserted()
        {
            // Arrange
            var basketId = Guid.NewGuid();
            var productListingId = Guid.NewGuid();
            var expected = Guid.NewGuid();
            var body = new AddProductListingInputModel
            {
                ProductListingId = productListingId,
                Amount = 1
            };
            _basketServiceMock.Setup(x => x.AddProductListingAsync(
                It.Is<Guid>(p => p == basketId), 
                It.Is<Guid>(p => p == productListingId), 
                It.Is<uint>(p => p == 1))).ReturnsAsync(expected);

            // Act
            var response = await _sut.AddProductListing(basketId, body);

            // Assert
            Assert.AreEqual(typeof(OkObjectResult), response.GetType());
            var actual = (OkObjectResult)response;
            Assert.AreEqual(expected, (Guid)actual.Value);
        }
        
        [Test]
        public async Task AddProductListing_ReturnBadRequestWithExceptionMessage_WhenEntityNotFound()
        {
            // Arrange
            var basketId = Guid.NewGuid();
            var productListingId = Guid.NewGuid();
            var body = new AddProductListingInputModel
            {
                ProductListingId = productListingId,
                Amount = 1
            };
            _basketServiceMock
                .Setup(x =>
                    x.AddProductListingAsync(
                        It.Is<Guid>(p => p == basketId), 
                        It.Is<Guid>(p => p == productListingId), 
                        It.Is<uint>(p => p == 1)))
                .Throws(new ArgumentException("NOT_FOUND"));

            // Act
            var response = await _sut.AddProductListing(basketId, body);

            // Assert
            Assert.AreEqual(typeof(BadRequestObjectResult), response.GetType());
            var actual = (BadRequestObjectResult)response;
            Assert.AreEqual("NOT_FOUND", (string)actual.Value);
        }
    }
}