

using ECommerce.Entities.Models;

namespace ECommerce.Core.Repositories.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
       Task<Category?> FindByNameAsync(string Name); 
    }
}
