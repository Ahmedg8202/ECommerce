using ECommerce.Core.Data;
using ECommerce.Core.Repositories.Interfaces;
using ECommerce.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Core.Repositories
{
    public class ProductCategoryRepository : GenericRepository<Product_Category>, IProductCategoryReository
    {
        public ProductCategoryRepository(ApplicationDbContext _context) : base(_context)
        {

        }

        public async Task<bool> AddProductCategories(int ProductId, List<int> CategoriesIds)
        {
            foreach(var id in CategoriesIds)
            {
                var ProductCategory = await _Dbset.FindAsync(ProductId, id);
                if(ProductCategory == null)
                {
                    await base.AddAsync(new Product_Category { ProductId = ProductId, CategoryId = id });
                }
            }
            return true;
        }

        public async Task<bool> RemoveProductCategories(int ProductId)
        {
            var ProductCategories = await _Dbset.Where(pc => pc.ProductId == ProductId).ToListAsync();
            foreach (var category in ProductCategories)
            {
                _Dbset.Remove(category);
            }
            return true;
        }
    }
}
