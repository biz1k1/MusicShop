namespace MusicShop.Presentation.Common.DTOs.Product
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int InStock { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
    }
}
