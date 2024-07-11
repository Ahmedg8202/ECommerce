using ECommerce.Entities.DtoModels.Create;
using ECommerce.Entities.DtoModels.Display;
using ECommerce.Entities.DtoModels.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface ICategoriesService
    {
        public Task<IEnumerable<DisplayCategory>> GetAllCategories();
        public Task<DisplayCategory> GetCategoryById(int Id);
        public Task<DisplayCategory> AddCategory(CreateCategory model);
        public Task<DisplayCategory> updateCategory(UpdateCategory model);
        public Task<bool> DeleteCategory(int Id);

    }
}
