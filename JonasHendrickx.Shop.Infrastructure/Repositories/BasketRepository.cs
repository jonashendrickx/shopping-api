using System;
using System.Threading.Tasks;
using JonasHendrickx.Shop.DataContext.Context;
using JonasHendrickx.Shop.Infrastructure.Contracts;
using JonasHendrickx.Shop.Models.Entities;
using Microsoft.EntityFrameworkCore;

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

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _dbContext.Baskets.FindAsync(id);
            _dbContext.Baskets.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Basket> GetAsync(Guid id)
        {
            var entity = await _dbContext.Baskets.FindAsync(id);
            return entity;
        }
    }
}