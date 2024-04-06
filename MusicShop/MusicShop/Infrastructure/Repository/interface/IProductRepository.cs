using MusicShop.Domain.Model.Core;

namespace MusicShop.Infrastructure.Repository
{
    public interface IProductRepository : IRepository<ProductEntity>
    {
        Task<IEnumerable<ProductEntity>> GetAllProductsAsync();
        Task<ProductEntity> GetProductByIdAsync(int id);
        Task<ProductEntity> GetProductIncludeCategoryByIdAsync(int id);
        IEnumerable<ProductEntity> GetProductsIncludeCategory();
        
    }
}
