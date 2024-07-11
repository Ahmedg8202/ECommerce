using ECommerce.Entities.DtoModels.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface ICartService
    {
        public Task<List<DisplayProduct>> GetProducts(string Username);
        public Task<bool> RemoveProduct(int Id, string Username);
    }
}
