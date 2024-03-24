using MusicShop.Domain.Model;

namespace MusicShop.Application.Services.FullTreeCategories;

public class FullTreeCategoriesService: IFullTreeCategoryService
{
    public IEnumerable<Category> CheckIfTheAreChildrenAndAddThem(int? Id, IEnumerable<Category> allCategories)
    {
        var children = allCategories.Where(x => x.ParentCategoryId == Id).ToList();
        foreach (var child in children)
        {
            child.ChildCategories = (ICollection<Category>)CheckIfTheAreChildrenAndAddThem(child.Id, allCategories);
        }
        return children;
    }


}
