using MusicShop.Domain.Model;

namespace MusicShop.Application.Services.FullTreeCategories;

public interface IFullTreeCategoryService
{
    IEnumerable<Category> CheckIfTheAreChildrenAndAddThem(int? Id, IEnumerable<Category> allCategories);
}
