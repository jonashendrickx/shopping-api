using System;
using System.Threading.Tasks;

namespace JonasHendrickx.Shop.Contracts
{
    public interface IDiscountService
    {
        Task<Guid> CreateAsync(string code, string rules, Guid productListingId);
    }
}