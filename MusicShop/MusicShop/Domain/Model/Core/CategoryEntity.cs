using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;

namespace MusicShop.Domain.Model.Core
{
    [Index(nameof(Name), IsUnique = true)]
    public class CategoryEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public CategoryEntity ParentCategory { get; set; }
        public virtual ICollection<CategoryEntity> ChildCategories { get; set; } = new List<CategoryEntity>();
        public virtual List<ProductEntity> Product { get; set; }
    }
}
