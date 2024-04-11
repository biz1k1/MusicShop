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

        public async Task<IEnumerable<CategoryEntity>> GetCategoryWithChildren(int id)
        {
            return await _dbContext.Categories.Where(x => x.Id == id).Include(x => x.ChildCategories).ThenInclude(x => x.ChildCategories).ToListAsync();
        }
        public async Task<IEnumerable<CategoryEntity>> CategoryWithProducts(int id)
        {
            return await _dbContext.Categories.Where(x=>x.Id==id).Include(x => x.Product).ToListAsync();
        }

    }
}
