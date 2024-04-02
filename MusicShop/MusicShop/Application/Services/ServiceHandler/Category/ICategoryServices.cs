using MusicShop.Domain.Model;

namespace MusicShop.Application.Services.ServiceHandler
{
    public interface ICategoryServicesHandler
    {
        IEnumerable<Category> GetFullTreeCategories();

    }
}
