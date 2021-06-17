using System;
using System.Threading.Tasks;

namespace JonasHendrickx.Shop.Contracts
{
    public interface IBasketService
    {
        Task<Guid> CreateAsync();
        Task DeleteAsync(Guid id);
        Task<decimal> GetAmountAsync(Guid id);
        Task<Guid> AddProductListingAsync(Guid basketId, Guid productListingId, uint amount);
    }
}