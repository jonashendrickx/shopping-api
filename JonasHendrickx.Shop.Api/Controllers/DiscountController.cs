using System;
using System.Threading.Tasks;
using JonasHendrickx.Shop.Api.Contracts.Discount;
using JonasHendrickx.Shop.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JonasHendrickx.Shop.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly ILogger<BasketController> _logger;
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService, ILogger<BasketController> logger)
        {
            _discountService = discountService ?? throw new ArgumentNullException(nameof(discountService));
            _logger = logger;
        }
        
        /// <summary>
        /// Create a discount for a product listing.
        /// </summary>
        /// <remarks></remarks>
        /// <response code="200">discountId:uuid.</response>
        /// <response code="500">Can't create a discount for the product listing right now.</response>
        [HttpPost]
        public async Task<IActionResult> Create(CreateDiscountInputModel inputModel)
        {
            var id = await _discountService.CreateAsync(inputModel.Code, inputModel.Rules, inputModel.ProductListingId);
            return Ok(id);
        }
    }
}