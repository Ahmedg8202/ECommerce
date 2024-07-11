
namespace ECommerce.Core.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICartRepository Cart { get; }
        ICategoryRepository Category { get; }
        IOrderRepository Order { get; }
        IProductService Product { get; }
        IProductCategoryReository ProductCategory { get; }
        IOrderProductRepository OrderProduct { get; }
        ICartProductRepository CartProduct { get; }
        Task<bool> Complete();

    }
}
