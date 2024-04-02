using MusicShop.Domain.Model;

namespace MusicShop.Infrastructure.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> GetProductIncludeCategoryByIdAsync(int id);
        IEnumerable<Product> GetProductsIncludeCategory();
        
    }
}
