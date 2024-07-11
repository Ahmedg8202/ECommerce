using Core.Services.Interfaces;
using ECommerce.Core.Repositories.Interfaces;
using ECommerce.Entities.DtoModels.Display;
using ECommerce.Entities.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unit;
        private readonly UserManager<AppUser> _userManager;
        public CartService(IUnitOfWork unit, UserManager<AppUser> userManager)
        {
            _unit = unit;
            _userManager = userManager;
        }
        public async Task<List<DisplayProduct>> GetProducts(string Username)
        {
            var user = await _userManager.FindByNameAsync(Username);
            if (user is null)
                return null;

            string UserId = user.Id;
            var Cart = await _unit.Cart.FindByUserId(UserId);
            if (Cart == null)
            {
                var Result = await _unit.Cart.AddAsync(new Cart { UserId = UserId });

                Result = await _unit.Complete();
                if (!Result) 
                    return null;
            }

            var CartProducts = await _unit.CartProduct.GetAllAsync(Cart.Id);

            var Displays = new List<DisplayProduct>();
            foreach (var pro in CartProducts)
            {
                var Pro = await _unit.Product.FindByIdAsync(pro.ProductId);
                var Display = new DisplayProduct
                {
                    Id = Pro.Id,
                    Name = Pro.Name,
                    Description = Pro.Description,
                    Price = Pro.Price,
                    Image = Pro.Image,
                    Categoris = Pro.Categories.Select(c => c.Name).ToList()
                };
                Displays.Add(Display);
            }
            return Displays;
        }

        public async Task<bool> RemoveProduct(int Id, string Username)
        {
            var user = await _userManager.FindByNameAsync(Username);
            if (user is null)
                return false;

            string UserId = user.Id;

            var Cart = await _unit.Cart.FindByUserId(UserId);
            if (Cart is null)
                return false;

            var result = await _unit.CartProduct.DeleteProductAsync(Id, Cart.Id);
            result = await _unit.Complete();
            if (!result)
                return false;

            return true;

        }
    }
}
