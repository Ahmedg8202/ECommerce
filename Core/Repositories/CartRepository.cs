using ECommerce.Core.Data;
using ECommerce.Core.Repositories.Interfaces;
using ECommerce.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Core.Repositories
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Cart?> FindByUserId(string Id)
        {
            return await _Dbset.FirstOrDefaultAsync(c => c.UserId == Id);
        }
    }
}
