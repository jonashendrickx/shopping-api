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
        
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var id = await _basketService.CreateAsync();
            return Ok(id);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(Guid id)
        {
            await _basketService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}/Amount")]
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

        [HttpPost("{basketId}/AddProductListing")]
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