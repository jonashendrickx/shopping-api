using System;
using System.Threading.Tasks;
using JonasHendrickx.Shop.Api.Contracts.Product;
using JonasHendrickx.Shop.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JonasHendrickx.Shop.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _logger = logger;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductInputModel inputModel)
        {
            var id = await _productService.CreateAsync(inputModel.Code, inputModel.Name);
            return Ok(id);
        }
    }
}