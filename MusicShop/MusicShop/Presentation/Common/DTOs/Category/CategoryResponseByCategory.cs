using MusicShop.Presentation.Common.DTOs.Product;

namespace MusicShop.Presentation.Common.DTOs.Category
{
    public record CategoryResponseByProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductResponse> products { get; set; }
    }
}
