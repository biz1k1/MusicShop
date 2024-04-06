using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MusicShop.Application.Services.DbInitializer;
using MusicShop.Domain.Model.Aunth;
using MusicShop.Domain.Model.Core;
using MusicShop.Infrastructure.Data.Configuration;

namespace MusicShop.Infrastructure.Data
{
    public class DataContext(DbContextOptions<DataContext> options, IOptions<AuthorizeOptions> authorizeOptions) : DbContext(options)
    {

        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RolePermissionConfiguration(authorizeOptions.Value));
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionsConfiguration());

        }

    }
}
