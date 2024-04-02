using Microsoft.EntityFrameworkCore;
using MusicShop.Domain.Model;
using MusicShop.Infrastructure.Data;

namespace MusicShop.Infrastructure.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(DataContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<Category>> GetAllCategoryAsync()
        {
            return await GetAll().ToListAsync();
            
        }
        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
        }



    }
}
