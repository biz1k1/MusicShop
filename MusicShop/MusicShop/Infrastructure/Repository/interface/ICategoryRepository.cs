using MusicShop.Domain.Model;

namespace MusicShop.Infrastructure.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetAllCategoryAsync();
        Task<Category> GetCategoryByIdAsync(int id);
    }
}
