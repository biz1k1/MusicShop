using MusicShop.Domain.Model.Core;

namespace MusicShop.Application.Services.FullTreeCategories;

public class FullTreeCategoriesService: IFullTreeCategoryService
{
    public IEnumerable<CategoryEntity> CheckIfTheAreChildrenAndAddThem(int? Id, IEnumerable<CategoryEntity> allCategories)
    {
        var children =  allCategories.Where(x => x.ParentCategoryId == Id).ToList();
        foreach (var child in children)
        {
            child.ChildCategories = (ICollection<CategoryEntity>)CheckIfTheAreChildrenAndAddThem(child.Id, allCategories);
        }
        return children;
    }


}
