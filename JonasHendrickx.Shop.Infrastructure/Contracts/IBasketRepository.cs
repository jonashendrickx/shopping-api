using System;
using System.Threading.Tasks;

namespace JonasHendrickx.Shop.Infrastructure.Contracts
{
    public interface IBasketRepository
    {
        Task<Guid> CreateAsync();
    }
}