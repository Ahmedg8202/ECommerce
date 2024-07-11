using ECommerce.Entities.DtoModels.Create;
using ECommerce.Entities.DtoModels.Display;
using ECommerce.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<IEnumerable<DisplayOrder>> GetOrders();
        public Task<DisplayOrder> GetOrder(int Id);
        public Task<List<CreateOrderProduct>> AddOrder(string Username, List<CreateOrderProduct> model);
        public Task<bool> DeleteOrder(int Id);
    }
}
