using MusicShop.Domain.Model.Core;

namespace MusicShop.Infrastructure.Repository
{
    public interface ICategoryRepository : IRepository<CategoryEntity>
    {
        Task<CategoryEntity?> GetCategoryWithChildren(int id);
        Task<CategoryEntity?> GetCategoryWithProducts(int id);
    }
}
