using Microsoft.EntityFrameworkCore;

namespace MusicShop.Infrastructure.Repository
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private ICategoryRepository _category;
        private IProductRepository _product;
        private IUserRepository _user;
        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ICategoryRepository Category
        {
            get
            {
                if (_category == null)
                {
                    _category = new CategoryRepository(_dbContext);
                }
                return _category;
            }
        }
        public IProductRepository Product
        {
            get
            {
                if (_product == null)
                {
                    _product = new ProductRepository(_dbContext);
                }
                return _product;
            }
        }
        public IUserRepository User
        {
            get
            {
                if(_user == null)
                {
                    _user = new UserRepository(_dbContext);
                }
                return _user;
            }
        }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

    }
}
