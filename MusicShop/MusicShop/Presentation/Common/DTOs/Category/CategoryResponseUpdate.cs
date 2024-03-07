using MusicShop.Domain.Model;
namespace MusicShop.Presentation.Common.DTOs.Category
{
    public record CategoryResponseUpdate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
