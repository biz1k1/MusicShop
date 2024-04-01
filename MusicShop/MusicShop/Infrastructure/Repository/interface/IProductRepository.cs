using MusicShop.Domain.Model;

namespace MusicShop.Infrastructure.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllCategoryAsenc();
        Task<Product> GetCategoryByCondition();
    }
}
