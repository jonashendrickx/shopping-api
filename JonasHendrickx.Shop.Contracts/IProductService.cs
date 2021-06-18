using System;
using System.Threading.Tasks;

namespace JonasHendrickx.Shop.Contracts
{
    public interface IProductService
    {
        Task<Guid> CreateAsync(string code, string name);
    }
}