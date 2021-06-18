using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JonasHendrickx.Shop.DataContext.Context;
using JonasHendrickx.Shop.Infrastructure.Contracts;
using JonasHendrickx.Shop.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace JonasHendrickx.Shop.Infrastructure.Repositories
{
    public class ProductListingRepository : IProductListingRepository
    {
        private readonly ShopDbContext _dbContext;

        public ProductListingRepository(ShopDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Guid> CreateAsync(Guid productId, decimal price, DateTime startDate, DateTime? endDate)
        {
            var entity = new ProductListing
            {
                ProductId = productId,
                Price = price,
                StartedAt = startDate,
                EndedAt = endDate
            };
            _dbContext.ProductListings.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<ICollection<ProductListing>> GetAsync()
        {
            return await _dbContext.ProductListings.Include(x => x.Product).ToListAsync();
        }

        public async Task<ProductListing> GetAsync(Guid id)
        {
            var entity = await _dbContext.ProductListings.SingleOrDefaultAsync(x => x.Id == id);
            return entity;
        }
    }
}