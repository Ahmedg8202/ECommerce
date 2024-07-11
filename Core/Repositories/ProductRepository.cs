using ECommerce.Core.Data;
using ECommerce.Core.Repositories;
using ECommerce.Core.Repositories.Interfaces;
using ECommerce.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceSystem.DataService.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductService
    {
        public ProductRepository(ApplicationDbContext _context) : base(_context)
        {
        }

        public override async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _Dbset.Include(p => p.Categories).ToListAsync();
        }

        public override async Task<Product?> FindByIdAsync(int Id)
        {
            return await _Dbset.Include(p => p.Categories).FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task<Product?> FindByNameAsync(string Name)
        {
            return await _Dbset.FirstOrDefaultAsync(p => p.Name == Name);
        }
    }
}
