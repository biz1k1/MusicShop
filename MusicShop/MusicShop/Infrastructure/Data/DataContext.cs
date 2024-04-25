using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MusicShop.Domain.Model.Aunth;
using MusicShop.Domain.Model.Core;
using MusicShop.Infrastructure.Data.Configuration;

namespace MusicShop.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        private readonly IOptions<AuthorizeOptions> _authorizeOptions;
        public DataContext(DbContextOptions<DataContext> options, IOptions<AuthorizeOptions> authorizeOptions) : base(options)
        {
            _authorizeOptions = authorizeOptions;
        }
        public DataContext()
        {

        }
        public virtual DbSet<CategoryEntity> Categories { get; set; }
        public virtual DbSet<ProductEntity> Products { get; set; }
        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<RoleEntity> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RolePermissionConfiguration(_authorizeOptions.Value));
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionsConfiguration());

        }

    }
}
