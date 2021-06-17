using System;
using System.Threading.Tasks;
using JonasHendrickx.Shop.DataContext.Context;
using JonasHendrickx.Shop.Infrastructure.Contracts;
using JonasHendrickx.Shop.Models.Entities;

namespace JonasHendrickx.Shop.Infrastructure.Repositories
{
    public class ProductListingRepository : IProductListingRepository
    {
        private readonly ShopDbContext _dbContext;

        public ProductListingRepository(ShopDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        
        public async Task<ProductListing> GetAsync(Guid id)
        {
            var entity = await _dbContext.ProductListings.FindAsync(id);
            return entity;
        }
    }
}