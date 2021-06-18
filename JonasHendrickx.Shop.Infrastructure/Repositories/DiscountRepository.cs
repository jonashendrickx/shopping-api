using System;
using System.Threading.Tasks;
using JonasHendrickx.Shop.DataContext.Context;
using JonasHendrickx.Shop.Infrastructure.Contracts;
using JonasHendrickx.Shop.Models.Entities;

namespace JonasHendrickx.Shop.Infrastructure.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly ShopDbContext _dbContext;

        public DiscountRepository(ShopDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Guid> CreateAsync(string code, string rules, Guid productListingId)
        {
            var entity = new Discount
            {
                Code = code,
                Rules = rules,
                ProductListingId = productListingId
            };
            _dbContext.Discounts.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }
    }
}