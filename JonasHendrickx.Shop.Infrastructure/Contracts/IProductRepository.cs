using System;
using System.Threading.Tasks;

namespace JonasHendrickx.Shop.Infrastructure.Contracts
{
    public interface IProductRepository
    {
        Task<Guid> CreateAsync(string code, string name);
    }
}