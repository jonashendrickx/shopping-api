using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace JonasHendrickx.Shop.Api.IntegrationTests.Controllers
{
    public class BasketControllerIntegrationTests : BaseControllerIntegrationTests
    {   
        [Test]
        public async Task ShouldBeAbleToCreateAndDeleteBasket()
        {
            // Act
            var basketId = await CreateBasketAsync();
            await DeleteBasketAsync(basketId);
        }
        
        [Test]
        public async Task AddPenToBasketAndGetAmount()
        {
            // Arrange
            var basketId = await CreateBasketAsync();
            var productId = await CreateProductAsync("PEN", "Lana Pen");
            var productListingId = await CreateProductListingAsync(3.0M, productId, DateTime.Today);
            var basketLineItemId = await AddProductListingToBasketAsync(basketId, productListingId, 3);
            
            // Act
            var actual = await GetBasketAmountAsync(basketId);
            
            // Assert
            Assert.AreEqual(9.0M, actual);
        }
        
        [Test]
        public async Task AddPenWithDiscountToBasketAndGetAmount()
        {
            // Arrange
            var basketId = await CreateBasketAsync();
            var penProductId = await CreateProductAsync("PEN", "Lana Pen");
            var penProductListingId = await CreateProductListingAsync(5.0M, penProductId, DateTime.Today);
            await CreateDiscountAsync("BUY_TO_FREE_QTY", "{\"buy_qty\":\"2\",\"free_qty\":\"1\"}", penProductListingId);
            await AddProductListingToBasketAsync(basketId, penProductListingId, 3);
            var shirtProductId = await CreateProductAsync("T-SHIRT", "Lana T-Shirt");
            var shirtProductListingId = await CreateProductListingAsync(20.0M, shirtProductId, DateTime.Today);
            await AddProductListingToBasketAsync(basketId, shirtProductListingId, 3);
            await CreateDiscountAsync("QTY_TO_PCT", "{\"qty\":\"3\",\"pct\":\"0.25\"}", shirtProductListingId);
            
            // Act
            var actual = await GetBasketAmountAsync(basketId);
            
            // Assert
            Assert.AreEqual(55M, actual);
        }
    }
}