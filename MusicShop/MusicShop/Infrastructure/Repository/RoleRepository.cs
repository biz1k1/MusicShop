using Microsoft.EntityFrameworkCore;
using MusicShop.Domain.Enums;
using MusicShop.Domain.Model.Aunth;
using MusicShop.Domain.Model.Core;
using MusicShop.Infrastructure.Data;
using System.Linq.Expressions;

namespace MusicShop.Infrastructure.Repository
{
    public class RoleRepository : Repository<RoleEntity>, IRoleRepository
    {
        private readonly DataContext _dbContext;
        public RoleRepository(DataContext DataContext) : base(DataContext)
        {
            _dbContext = DataContext;
        }

    }
}
