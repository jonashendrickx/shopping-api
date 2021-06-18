using System;
using System.Threading.Tasks;
using JonasHendrickx.Shop.DataContext.Context;
using JonasHendrickx.Shop.Infrastructure.Contracts;
using JonasHendrickx.Shop.Models.Entities;

namespace JonasHendrickx.Shop.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        
        private readonly ShopDbContext _dbContext;

        public ProductRepository(ShopDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Guid> CreateAsync(string code, string name)
        {
            var entity = new Product
            {
                Code = code,
                Name = name
            };
            _dbContext.Products.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }
    }
}