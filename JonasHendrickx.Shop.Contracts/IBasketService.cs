using System;
using System.Threading.Tasks;

namespace JonasHendrickx.Shop.Contracts
{
    public interface IBasketService
    {
        Task<Guid> CreateAsync();
    }
}