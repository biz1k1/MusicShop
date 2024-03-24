using MusicShop.Domain.Model;

namespace MusicShop.Presentation.Common.DTOs.Product
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public int InStock { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
    }
}
