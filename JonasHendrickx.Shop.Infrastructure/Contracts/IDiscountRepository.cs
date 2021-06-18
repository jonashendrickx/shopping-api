using System;
using System.Threading.Tasks;

namespace JonasHendrickx.Shop.Infrastructure.Contracts
{
    public interface IDiscountRepository
    {
        Task<Guid> CreateAsync(string code, string rules, Guid productListingId);
    }
}