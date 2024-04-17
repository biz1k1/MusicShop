using MusicShop.Domain.Model;
using System.Threading.Channels;
namespace MusicShop.Presentation.Common.DTOs.Category
{
    public record CategoryRequestUpdate
    {
        public int  CategoryToChangeId{ get; set; }
        public string? Name { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
