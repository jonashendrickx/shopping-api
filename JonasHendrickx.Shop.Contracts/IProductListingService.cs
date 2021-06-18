using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JonasHendrickx.Shop.Contracts
{
    public interface IProductListingService
    {
        Task<Guid> CreateAsync(Guid productId, decimal price, DateTime startDate, DateTime? endDate);
        Task<ICollection<ProductListingInfo>> GetAsync();
    }
}