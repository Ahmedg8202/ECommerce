

using ECommerce.Entities.Models;

namespace ECommerce.Core.Repositories.Interfaces
{
    public interface IProductCategoryReository : IGenericRepository<Product_Category>
    {
        Task<bool> AddProductCategories(int ProductId, List<int> CategoriesIds);

        Task<bool> RemoveProductCategories(int ProductId);
    }
}
