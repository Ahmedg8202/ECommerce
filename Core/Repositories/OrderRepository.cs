using ECommerce.Core.Data;
using ECommerce.Core.Repositories.Interfaces;
using ECommerce.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Core.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext _context) : base(_context)
        {
        }

        public async Task<IEnumerable<Order>> GetByDate(DateTime date)
        {
            return await _Dbset.Where(o => o.CreatedOn == date).ToListAsync();
        }
    }
}
