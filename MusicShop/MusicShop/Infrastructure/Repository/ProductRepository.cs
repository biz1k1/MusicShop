using Microsoft.EntityFrameworkCore;
using MusicShop.Domain.Model;

namespace MusicShop.Infrastructure.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DbContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<Product>> GetAllCategoryAsync()
        {
            return await GetAll().ToListAsync();

        }
        public async Task<Product> GetCategoryByIdAsync(int id)
        {
            return await FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
