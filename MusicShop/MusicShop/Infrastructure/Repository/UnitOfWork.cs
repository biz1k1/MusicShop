﻿using Microsoft.EntityFrameworkCore;
using MusicShop.Infrastructure.Data;

namespace MusicShop.Infrastructure.Repository
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly DataContext _dbContext;
        private ICategoryRepository _category;
        private IProductRepository _product;
        private IUserRepository _user;
        private IRoleRepository _role;
        public UnitOfWork(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IRoleRepository Role
        {
            get
            {
                if (_role == null)
                {
                    _role = new RoleRepository(_dbContext);
                }
                return _role;
            }
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
