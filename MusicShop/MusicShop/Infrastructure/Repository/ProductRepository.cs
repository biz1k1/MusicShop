using Microsoft.EntityFrameworkCore;
using MusicShop.Domain.Model;
using MusicShop.Infrastructure.Data;
using System.CodeDom.Compiler;

namespace MusicShop.Infrastructure.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DataContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await GetAll().ToListAsync();

        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Product> GetProductIncludeCategoryByIdAsync(int id)
        {
            return await FindByCondition(x => x.Id == id).Include(x=>x.Category).FirstOrDefaultAsync();
        }

        public IEnumerable<Product> GetProductsIncludeCategory()
        {
            return  GetAll().Include(x => x.Category).ToList();
        }
    }
}
