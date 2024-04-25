using Microsoft.EntityFrameworkCore;
using MusicShop.Domain.Model.Core;
using MusicShop.Infrastructure.Data;
using System.CodeDom.Compiler;

namespace MusicShop.Infrastructure.Repository
{
    public class ProductRepository : Repository<ProductEntity>, IProductRepository
    {
        public ProductRepository(DataContext dbContext) : base(dbContext)
        {
        }
        public async Task<ProductEntity?> GetProductIncludeCategoryByIdAsync(int id)
        {
            return await _dbContext.Products.AsNoTracking().Where(x => x.Id == id).Include(x=>x.Category).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ProductEntity>> GetProductsIncludeCategoryAsync()
        {
            return await _dbContext.Products.Include(x => x.Category).ToListAsync();
        }
    }
}
