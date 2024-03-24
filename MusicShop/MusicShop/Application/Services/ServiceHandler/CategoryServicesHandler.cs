using MusicShop.Application.Services.FullTreeCategories;
using MusicShop.Domain.Model;
using MusicShop.Infrastructure.Data;


namespace MusicShop.Application.Services.ServiceHandler
{
    public class CategoryServicesHandler : ICategoryServicesHandler
    {
        private readonly DataContext _db;
        private readonly IFullTreeCategoryService _fullTreeCategories;
        public CategoryServicesHandler(IFullTreeCategoryService fullTreeCategories, DataContext dataContext)
        {
            _fullTreeCategories = fullTreeCategories;
            _db = dataContext;
        }

        public IEnumerable<Category> GetFullTreeCategories()
        {
            var allCategories = _db.Categories.ToList();
            return _fullTreeCategories.CheckIfTheAreChildrenAndAddThem(null, allCategories);
        }
    }
}
