using System;
using System.Threading.Tasks;
using JonasHendrickx.Shop.Models.Entities;

namespace JonasHendrickx.Shop.Infrastructure.Contracts
{
    public interface IProductListingRepository
    {
        Task<ProductListing> GetAsync(Guid id);
    }
}