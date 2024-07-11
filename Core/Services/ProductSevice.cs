using Core.Services.Interfaces;
using ECommerce.Core.Repositories.Interfaces;
using ECommerce.Entities.DtoModels.Create;
using ECommerce.Entities.DtoModels.Display;
using ECommerce.Entities.DtoModels.Update;
using ECommerce.Entities.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ECommerce.Core.Services
{
    public class ProductSevice : IProductSevice
    {
        private readonly IUnitOfWork _unit;
        private readonly UserManager<AppUser> _userManager;

        public ProductSevice(IUnitOfWork _unit, UserManager<AppUser> _userManager)
        {
            this._unit = _unit;
            this._userManager = _userManager;
        }
        public async Task<List<DisplayProduct>> GetAllProducts()
        {
            var Products = await _unit.Product.GetAllAsync();
            var Displays = new List<DisplayProduct>();
            foreach (var P in Products)
            {
                var Display = new DisplayProduct
                {
                    Id = P.Id,
                    Name = P.Name,
                    Description = P.Description,
                    Price = P.Price,
                    Image = P.Image,
                    Categoris = P.Categories.Select(c => c.Name).ToList()
                };
                Displays.Add(Display);
            }
            return Displays;
        }
        public async Task<DisplayProduct> GetProduct(int Id)
        {
            var Product = await _unit.Product.FindByIdAsync(Id);
            if (Product == null)
                return null;

            var Display = new DisplayProduct
            {
                Id = Product.Id,
                Name = Product.Name,
                Description = Product.Description,
                Price = Product.Price,
                Image = Product.Image,
                Categoris = Product.Categories.Select(c => c.Name).ToList()
            };
            return Display;
        }
        public async Task<DisplayProduct> AddProduct(CreateProduct model)
        {
            var Product = await _unit.Product.FindByNameAsync(model.Name);
            if (Product != null)
                return new DisplayProduct { Name ="Registered"};

            var Categories = new List<Category>();
            foreach (var cate in model.Categories)
            {
                var category = await _unit.Category.FindByNameAsync(cate);
                if (category == null)
                    continue;
                Categories.Add(category);
            }

            using var stream = new MemoryStream();
            await model.Image.CopyToAsync(stream);

            Product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Image = stream.ToArray()
            };

            var Result = await _unit.Product.AddAsync(Product);
            
            Result = await _unit.Complete();
            if (!Result)
                return null;

            Result = await _unit.ProductCategory.AddProductCategories(Product.Id, Categories.Select(c => c.Id).ToList());
            
            Result = await _unit.Complete();
            if (!Result)
                return null;

            var Display = new DisplayProduct
            {
                Id = Product.Id,
                Name = Product.Name,
                Description = Product.Description,
                Price = Product.Price,
                Image = Product.Image,
                Categoris = Categories.Select(c => c.Name).ToList()
            };

            return Display;
        }
        public async Task<bool> AddProductToCart(string Username, int Id)
        {
            var product = await _unit.Product.FindByIdAsync(Id);
            var user = await _userManager.FindByNameAsync(Username);
            if (product == null || user == null)
                return false;

            string UserId = user.Id;
            var Cart = await _unit.Cart.FindByUserId(UserId);
            if (Cart == null)
            {
                var Result = await _unit.Cart.AddAsync(new Cart { UserId = UserId });
                Result = await _unit.Complete();
                if (!Result)
                    return false;
            }
            
            var result = await _unit.CartProduct.AddAsync(new Cart_Product
            {
                CartId = Cart.Id,
                ProductId = Id
            });
            result = await _unit.Complete();
            if (!result)
                return false;

            return true;
        }
        public async Task<DisplayProduct> UpdateProduct(UpdateProduct model)
        {
            var Product = await _unit.Product.FindByIdAsync(model.Id);
            if (Product == null)
                return new DisplayProduct { Name = "Not Registered"};

            if (!string.IsNullOrEmpty(model.Name) && model.Name != Product.Name)
                Product.Name = model.Name;

            if (!string.IsNullOrEmpty(model.Description) && model.Description != Product.Description)
                Product.Description = model.Description;

            if (model.Price != 0 && model.Price != Product.Price)
                Product.Price = model.Price;

            if (model.Image != null)
            {
                using var Stream = new MemoryStream();
                await model.Image.CopyToAsync(Stream);
                Product.Image = Stream.ToArray();
            }

            if (model.Categories != null && model.Categories.Count != 0)
            {
                var Categories = new List<Category>();
                foreach (var cate in model.Categories)
                {
                    var category = await _unit.Category.FindByNameAsync(cate);
                    if (category == null)
                        continue;
                    Categories.Add(category);
                }
                var Result = await _unit.ProductCategory.RemoveProductCategories(model.Id);
                Result = await _unit.ProductCategory.AddProductCategories(model.Id, Categories.Select(c => c.Id).ToList());
                if (!Result)
                    return null;
            }
            var result = _unit.Product.Update(Product);
            result = await _unit.Complete();
            if (!result)
                return null;

            var Display = new DisplayProduct
            {
                Id = Product.Id,
                Name = Product.Name,
                Description = Product.Description,
                Price = Product.Price,
                Image = Product.Image,
                Categoris = Product.Categories.Select(c => c.Name).ToList()
            };

            return Display;
        }
        public async Task<bool> DeleteProduct(int Id)
        {
            var Result = await _unit.Product.DeleteAsync(Id);
            Result = await _unit.Complete();
            if (!Result)
                return false;

            return true;
        }
    }
}
