using MusicShop.Domain.Model.Core;

namespace MusicShop.Application.Services.ServiceHandler
{
    public interface ICategoryServicesHandler
    {
        IEnumerable<CategoryEntity> GetFullTreeCategories();

    }
}
