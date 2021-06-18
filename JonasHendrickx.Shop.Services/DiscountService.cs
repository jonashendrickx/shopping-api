using System;
using System.Threading.Tasks;
using JonasHendrickx.Shop.Contracts;
using JonasHendrickx.Shop.Infrastructure.Contracts;

namespace JonasHendrickx.Shop.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        
        public DiscountService(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository ?? throw new ArgumentNullException(nameof(discountRepository));
        }
        
        public async Task<Guid> CreateAsync(string code, string rules, Guid productListingId)
        {
            var id = await _discountRepository.CreateAsync(code, rules, productListingId);
            return id;
        }
    }
}