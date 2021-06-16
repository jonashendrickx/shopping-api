using System.Threading.Tasks;
using JonasHendrickx.Shop.Contracts;
using NUnit.Framework;
using Moq;
using System;
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
        public async Task Create_ReturnsId_WhenBaskedIsCreated()
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
    }
}