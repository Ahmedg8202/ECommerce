using ECommerce.Entities.DtoModels.Create;
using ECommerce.Entities.DtoModels.Display;
using ECommerce.Entities.DtoModels.Update;
namespace Core.Services.Interfaces
{
    public interface IProductSevice
    {
        public Task<List<DisplayProduct>> GetAllProducts();
        public Task<DisplayProduct> GetProduct(int Id);
        public Task<DisplayProduct> AddProduct(CreateProduct model);
        public Task<bool> AddProductToCart(string username, int Id);
        public Task<DisplayProduct> UpdateProduct(UpdateProduct model);
        public Task<bool> DeleteProduct(int Id);
    }
}
