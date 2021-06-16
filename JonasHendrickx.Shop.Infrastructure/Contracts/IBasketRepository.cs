using System;
using System.Threading.Tasks;
using JonasHendrickx.Shop.Models.Entities;

namespace JonasHendrickx.Shop.Infrastructure.Contracts
{
    public interface IBasketRepository
    {
        Task<Guid> CreateAsync();
        Task DeleteAsync(Guid id);
        Task<Basket> GetAsync(Guid id);
    }
}