using System;
using System.Threading.Tasks;
using JonasHendrickx.Shop.Contracts;
using JonasHendrickx.Shop.Infrastructure.Contracts;

namespace JonasHendrickx.Shop.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        
        public BasketService(IBasketRepository basketRepository)
    {
            _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
        }
        
        public async Task<Guid> CreateAsync()
        {
            var id = await _basketRepository.CreateAsync();
            return id;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _basketRepository.DeleteAsync(id);
        }
    }
}