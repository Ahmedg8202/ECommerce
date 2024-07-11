using Core.Services.Interfaces;
using ECommerce.Core.Repositories.Interfaces;
using ECommerce.Entities.DtoModels.Create;
using ECommerce.Entities.DtoModels.Display;
using ECommerce.Entities.DtoModels.Update;
using ECommerce.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IUnitOfWork _unit;

        public CategoriesService(IUnitOfWork _unit)
        {
            this._unit = _unit;
        }

        public async Task<IEnumerable<DisplayCategory>> GetAllCategories()
        {
            var Categories = await _unit.Category.GetAllAsync();
            var Display = Categories.Select(cat => new DisplayCategory
            {
                Id = cat.Id,
                Name = cat.Name,
                Description = cat.Description
            });
            return Display;
        }
        public async Task<DisplayCategory> GetCategoryById(int Id)
        {
            var Category = await _unit.Category.FindByIdAsync(Id);
            if (Category == null)
                return null;

            var Display = new DisplayCategory
            {
                Id = Category.Id,
                Name = Category.Name,
                Description = Category.Description
            };
            return Display;
        }
        public async Task<DisplayCategory> AddCategory(CreateCategory model)
        {
            if (await _unit.Category.FindByNameAsync(model.Name) != null)
                return new DisplayCategory { Name = "Rgistered" };

            var Category = new Category
            {
                Name = model.Name,
                Description = model.Description
            };

            var Result = await _unit.Category.AddAsync(Category);
            Result = await _unit.Complete();
            if (!Result)
                return null;

            var Display = new DisplayCategory
            {
                Id = Category.Id,
                Name = Category.Name,
                Description = Category.Description
            };
            return Display;
        }
        public async Task<DisplayCategory> updateCategory(UpdateCategory model)
        {
            var Category = await _unit.Category.FindByIdAsync(model.Id);
            if (Category == null)
                return new DisplayCategory { Name = "Not Rgistered" };

            if (!string.IsNullOrEmpty(model.Name) && Category.Name != model.Name)
                Category.Name = model.Name;

            if (!string.IsNullOrEmpty(model.Description) && Category.Description != model.Description)
                Category.Description = model.Description;

            var Result = _unit.Category.Update(Category);
            Result = await _unit.Complete();
            if (!Result)
                return null;

            var Display = new DisplayCategory
            {
                Id = Category.Id,
                Name = Category.Name,
                Description = Category.Description
            };
            return Display;
        }
        public async Task<bool> DeleteCategory(int Id)
        {
            var Result = await _unit.Category.DeleteAsync(Id);
            Result = await _unit.Complete();

            if (!Result)
                return false;

            return true;
        }
    }
}
