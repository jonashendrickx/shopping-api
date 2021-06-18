using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JonasHendrickx.Shop.Contracts;
using JonasHendrickx.Shop.Infrastructure.Contracts;

namespace JonasHendrickx.Shop.Services
{
    public class ProductListingService : IProductListingService
    {
        private readonly IProductListingRepository _productListingRepository;
        
        public ProductListingService(IProductListingRepository productListingRepository)
        {
            _productListingRepository = productListingRepository ?? throw new ArgumentNullException(nameof(productListingRepository));
        }

        public async Task<Guid> CreateAsync(Guid productId, decimal price, DateTime startDate, DateTime? endDate)
        {
            var id = await _productListingRepository.CreateAsync(productId, price, startDate, endDate);
            return id;
        }

        public async Task<ICollection<ProductListingInfo>> GetAsync()
        {
            var entities = await _productListingRepository.GetAsync();
            return entities.Select(x => new ProductListingInfo
            {
                Id = x.Id,
                Code = x.Product.Code,
                Name = x.Product.Name
            }).ToList();
        }
    }
}