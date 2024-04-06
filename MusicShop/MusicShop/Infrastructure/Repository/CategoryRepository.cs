using Microsoft.EntityFrameworkCore;
using MusicShop.Domain.Model.Core;
using MusicShop.Infrastructure.Data;

namespace MusicShop.Infrastructure.Repository
{
    public class CategoryRepository : Repository<CategoryEntity>, ICategoryRepository
    {
        public CategoryRepository(DataContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<CategoryEntity>> GetAllCategoryAsync()
        {
            return await GetAll().ToListAsync();
            
        }
        public async Task<CategoryEntity> GetCategoryByIdAsync(int id)
        {
            return await FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
        }



    }
}
