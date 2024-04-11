using MusicShop.Domain.Model.Core;

namespace MusicShop.Infrastructure.Repository
{
    public interface ICategoryRepository : IRepository<CategoryEntity>
    {
        Task<IEnumerable<CategoryEntity>> GetCategoryWithChildren(int id);
        Task<IEnumerable<CategoryEntity>> CategoryWithProducts(int id);
    }
}
