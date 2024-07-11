using ECommerce.Core.Data;
using ECommerce.Core.Repositories.Interfaces;
using ECommerce.Entities.Models;
using Microsoft.EntityFrameworkCore;
namespace ECommerce.Core.Repositories
{
    public class CartProductRepository : GenericRepository<Cart_Product>, ICartProductRepository
    {
        public CartProductRepository(ApplicationDbContext _context) : base(_context)
        {
        }

        public async Task<IEnumerable<Cart_Product?>?> GetAllAsync(int Id)
        {
            return await _Dbset.ToListAsync();
        }

        public async Task<bool> DeleteProductAsync(int productId, int cartId)
        {
            var product = await _Dbset.FindAsync(productId, cartId);
            if (product == null)
                return false;
            _Dbset.Remove(product);
            return true;
        }
    }
}
