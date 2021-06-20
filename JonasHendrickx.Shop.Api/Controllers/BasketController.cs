using System;
using System.Threading.Tasks;
using JonasHendrickx.Shop.Api.Contracts.Basket;
using JonasHendrickx.Shop.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JonasHendrickx.Shop.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly ILogger<BasketController> _logger;
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService, ILogger<BasketController> logger)
        {
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
            _logger = logger;
        }
        
        /// <summary>
        /// Create a basket.
        /// </summary>
        /// <remarks></remarks>
        /// <response code="200">basketId:uuid.</response>
        /// <response code="500">Oops! Can't create your basket right now.</response>
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var id = await _basketService.CreateAsync();
            return Ok(id);
        }

        /// <summary>
        /// Delete a basket.
        /// </summary>
        /// <remarks></remarks>
        /// <param name="id">basketId</param>
        /// <response code="200">Basket created.</response>
        /// <response code="500">Oops! Can't delete your basket right now.</response>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _basketService.DeleteAsync(id);
            return NoContent();
        }
        
        /// <summary>
        /// Get value of items in basket.
        /// </summary>
        /// <remarks></remarks>
        /// <response code="200">amount:number.</response>
        /// <response code="400">Oops! A wrong parameter was supplied. Can't calculate the value of your basket right now.</response>
        /// <response code="500">Oops! Can't calculate the value of your basket right now.</response>
        [HttpGet("{id:guid}/Amount")]
        public async Task<IActionResult> GetAmount(Guid id)
        {
            try
            {
                var amount = await _basketService.GetAmountAsync(id);
                return Ok(amount);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Add a product listing to a basket.
        /// </summary>
        /// <remarks></remarks>
        /// <response code="200">productListingId:uuid.</response>
        /// <response code="400">Oops! A wrong parameter was supplied. Can't add a product listing to your basket right now.</response>
        /// <response code="500">Can't add a product listing to your basket right now.</response>
        [HttpPost("{basketId:guid}/AddProductListing")]
        public async Task<IActionResult> AddProductListing(Guid basketId, AddProductListingInputModel body)
        {
            try
            {
                var basketLineItemId = await _basketService.AddProductListingAsync(basketId, body.ProductListingId, body.Amount);
                return Ok(basketLineItemId);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}