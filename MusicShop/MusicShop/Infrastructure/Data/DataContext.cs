using Microsoft.EntityFrameworkCore;
using MusicShop.Domain.Model;

namespace MusicShop.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        //public DataContext() { }
        public DbSet<Category> Categories { get; set; } 
        public DbSet<Product> Products { get; set; } 
        public  DbSet<User> Users { get; set; } 

        
    }
}
