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
        public async Task<IEnumerable<ProductEntity>> GetAllProductsAsync()
        {
            return await GetAll().ToListAsync();

        }
        public async Task<ProductEntity> GetProductByIdAsync(int id)
        {
            return await FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<ProductEntity> GetProductIncludeCategoryByIdAsync(int id)
        {
            return await FindByCondition(x => x.Id == id).Include(x=>x.Category).FirstOrDefaultAsync();
        }

        public IEnumerable<ProductEntity> GetProductsIncludeCategory()
        {
            return  GetAll().Include(x => x.Category).ToList();
        }
    }
}
