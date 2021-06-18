﻿using System;
using System.Linq;
using System.Threading.Tasks;
using JonasHendrickx.Shop.Api.Contracts.ProductListing;
using JonasHendrickx.Shop.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JonasHendrickx.Shop.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductListingController : ControllerBase
    {
        private readonly ILogger<ProductListingController> _logger;
        private readonly IProductListingService _productListingService;

        public ProductListingController(IProductListingService productListingService, ILogger<ProductListingController> logger)
        {
            _productListingService = productListingService ?? throw new ArgumentNullException(nameof(productListingService));
            _logger = logger;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductListingInputModel inputModel)
        {
            var id = await _productListingService.CreateAsync(inputModel.ProductId, inputModel.Price, inputModel.StartDate, inputModel.EndDate);
            return Ok(id);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var productListings = await _productListingService.GetAsync();
                var response = productListings.Select(x => new ProductListingResponse
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name
                }).ToList();
                return Ok(response);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}