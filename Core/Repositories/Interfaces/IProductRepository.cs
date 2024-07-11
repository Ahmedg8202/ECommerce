
using ECommerce.Entities.Models;

namespace ECommerce.Core.Repositories.Interfaces
{
    public interface IProductService : IGenericRepository<Product>
    {
        Task<Product?> FindByNameAsync(string Name);
    }
}
