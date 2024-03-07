namespace MusicShop.Presentation.Common.DTOs.Category
{
    public record CategoryRequest
    {
        public string Name { get; set; }
        public int SubCategoryId { get; set; }
    }
}
