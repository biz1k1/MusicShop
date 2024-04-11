using MusicShop.Domain.Model.Core;

namespace MusicShop.Infrastructure.Repository
{
    public interface IProductRepository : IRepository<ProductEntity>
    {
        Task<ProductEntity> GetProductIncludeCategoryByIdAsync(int id);
        Task<IEnumerable<ProductEntity>> GetProductsIncludeCategoryAsync();


    }
}
