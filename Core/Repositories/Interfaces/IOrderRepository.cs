

using ECommerce.Entities.Models;

namespace ECommerce.Core.Repositories.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IEnumerable<Order>> GetByDate(DateTime date);
        
    }
}
