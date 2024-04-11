using MusicShop.Application.Services.FullTreeCategories;
using MusicShop.Application.Services.ServiceHandler;
using MusicShop.Domain.Model.Core;
using MusicShop.Infrastructure.Data;
using MusicShop.Infrastructure.Repository;

namespace MusicShop.Application.Services.ServiceHandler
{
    public class CategoryServicesHandler : ICategoryServicesHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFullTreeCategoryService _fullTreeCategories;
        public CategoryServicesHandler(IFullTreeCategoryService fullTreeCategories, IUnitOfWork dataContext)
        {
            _fullTreeCategories = fullTreeCategories;
            _unitOfWork = dataContext;
        }

        public async Task<IEnumerable<CategoryEntity>> GetFullTreeCategories()
        {
            var allCategories = await _unitOfWork.Category.GetAllAsync();
            return _fullTreeCategories.CheckIfTheAreChildrenAndAddThem(null, allCategories);
        }
    }
}
