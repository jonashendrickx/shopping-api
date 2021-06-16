using System;
using System.Threading.Tasks;
using JonasHendrickx.Shop.DataContext.Context;
using JonasHendrickx.Shop.Infrastructure.Contracts;
using JonasHendrickx.Shop.Models.Entities;

namespace JonasHendrickx.Shop.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly ShopDbContext _dbContext;

        public BasketRepository(ShopDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        
        public async Task<Guid> CreateAsync()
        {
            var entity = new Basket();
            _dbContext.Baskets.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }
    }
}