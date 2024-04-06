using MusicShop.Domain.Model.Core;

namespace MusicShop.Application.Services.FullTreeCategories;

public interface IFullTreeCategoryService
{
    IEnumerable<CategoryEntity> CheckIfTheAreChildrenAndAddThem(int? Id, IEnumerable<CategoryEntity> allCategories);
}
