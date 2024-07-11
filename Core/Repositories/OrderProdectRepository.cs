using ECommerce.Core.Data;
using ECommerce.Core.Repositories.Interfaces;
using ECommerce.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Core.Repositories
{
    public class OrderProdectRepository : GenericRepository<Order_Product>, IOrderProductRepository
    {
        public OrderProdectRepository(ApplicationDbContext _context) : base(_context)
        {
        }
    }
}
