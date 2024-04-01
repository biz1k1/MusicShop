using MusicShop.Domain.Model;

namespace MusicShop.Infrastructure.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllCategoryAsync();
        Task<Product> GetCategoryByIdAsync(int id);
    }
}
