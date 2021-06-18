using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JonasHendrickx.Shop.Models.Entities;

namespace JonasHendrickx.Shop.Infrastructure.Contracts
{
    public interface IProductListingRepository
    {
        Task<Guid> CreateAsync(Guid productId, decimal price, DateTime startDate, DateTime? endDate);
        Task<ICollection<ProductListing>> GetAsync();
        Task<ProductListing> GetAsync(Guid id);
    }
}