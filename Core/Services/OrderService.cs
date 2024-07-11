using Core.Services.Interfaces;
using ECommerce.Core.Repositories.Interfaces;
using ECommerce.Entities.DtoModels.Create;
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
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unit;
        private readonly UserManager<AppUser> _userManager;

        public OrderService(IUnitOfWork _unit, UserManager<AppUser> _userManager)
        {
            this._unit = _unit;
            this._userManager = _userManager;
        } 
        public async Task<List<CreateOrderProduct>> AddOrder(string Username, List<CreateOrderProduct> model)
        {
            var user = await _userManager.FindByNameAsync(Username);
            if (user == null)
                return null;

            var Cart = await _unit.Cart.FindByUserId(user.Id);
            if (Cart == null)
                return null;

            var CartProducts = await _unit.CartProduct.GetAllAsync(Cart.Id);
            if (CartProducts == null)
                return null;

            decimal totalPrice = 0;
            foreach (var Pro in model)
            {

                totalPrice += (Pro.Amount * Pro.Price);
            }
            var Order = new Order
            {
                TotalPrice = totalPrice,
                Done = false,
                CreatedOn = DateTime.Now,
                UserId = user.Id,
                Phone = user.PhoneNumber
            };
            var Result = await _unit.Order.AddAsync(Order);
            Result = await _unit.Complete();
            if (!Result)
                return null;

            var OrderProducts = new List<Order_Product>();
            foreach (var Pro in model)
            {
                var OrderProdect = new Order_Product
                {
                    OrderId = Order.Id,
                    ProductId = Pro.Id,
                    Price = Pro.Price,
                    Amount = Pro.Amount,
                    TotalPrice = Pro.Price * Pro.Amount
                };
                Result = await _unit.OrderProduct.AddAsync(OrderProdect);
            }
            Result = await _unit.Complete();
            if (!Result)
                return null;

            foreach (var pro in CartProducts)
            {
                Result = await _unit.CartProduct.DeleteProductAsync(pro.ProductId, pro.CartId);
            }
            Result = await _unit.Complete();
            if (!Result)
                return null;

            return model;
        }

        public async Task<bool> DeleteOrder(int Id)
        {
            var Result = await _unit.Order.DeleteAsync(Id);
            Result = await _unit.Complete();

            if (!Result)
                return false;

            return true;
        }

        public async Task<DisplayOrder> GetOrder(int Id)
        {
            var Order = await _unit.Order.FindByIdAsync(Id);
            if (Order == null)
                return null;

            var Display = new DisplayOrder
            {
                Id = Order.Id,
                Phone = Order.Phone,
                TotalPrice = Order.TotalPrice,
                Done = Order.Done,
                CreatedOn = Order.CreatedOn,
                DeliveredOn = Order.DeliveredOn,
                UserId = Order.UserId
            };
            return Display;
        }

        public async Task<IEnumerable<DisplayOrder>> GetOrders()
        {
            var Orders = await _unit.Order.GetAllAsync();
            var Display = Orders.Select(o => new DisplayOrder
            {
                Id = o.Id,
                Phone = o.Phone,
                TotalPrice = o.TotalPrice,
                Done = o.Done,
                CreatedOn = o.CreatedOn,
                DeliveredOn = o.DeliveredOn,
                UserId = o.UserId
            });
            return Display;
        }
    }
}
