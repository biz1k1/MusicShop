using Microsoft.EntityFrameworkCore;
using MusicShop.Domain.Model;

namespace MusicShop.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        
    }
}
