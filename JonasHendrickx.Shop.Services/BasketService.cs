using System;
using System.Linq;
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

        public async Task<decimal> GetAmountAsync(Guid id)
        {
            var basket = await _basketRepository.GetAsync(id);

            if (basket.LineItems == null || !basket.LineItems.Any())
            {
                return 0;
            }

            var result = basket.LineItems.Sum(x => x.Amount * x.ProductListing.Price);
            return result;
        }
    }
}