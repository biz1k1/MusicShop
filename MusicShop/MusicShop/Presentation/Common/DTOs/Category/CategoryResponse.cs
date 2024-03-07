namespace MusicShop.Presentation.Common.DTOs.Category
{
    public record CategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CategoryResponse> ChildCategories { get; set; } = new List<CategoryResponse>();
    }
}
