using MusicShop.Domain.Model.Core;

namespace MusicShop.Infrastructure.Repository
{
    public interface ICategoryRepository : IRepository<CategoryEntity>
    {
        Task<IEnumerable<CategoryEntity>> GetAllCategoryAsync();
        Task<CategoryEntity> GetCategoryByIdAsync(int id);
    }
}
