

using ECommerce.Entities.Models;

namespace ECommerce.Core.Repositories.Interfaces
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        Task<Cart?> FindByUserId(string Id);

    }
}
