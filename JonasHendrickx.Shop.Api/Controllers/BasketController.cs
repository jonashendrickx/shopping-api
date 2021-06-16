using System;
using System.Threading.Tasks;
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
    }
}