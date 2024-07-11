using ECommerce.Entities.Models;

namespace ECommerce.Core.Repositories.Interfaces
{
    public interface ICartProductRepository : IGenericRepository<Cart_Product>
    {
        Task<IEnumerable<Cart_Product?>?> GetAllAsync(int Id);

        Task<bool> DeleteProductAsync(int productId, int cartId);
    }
}
