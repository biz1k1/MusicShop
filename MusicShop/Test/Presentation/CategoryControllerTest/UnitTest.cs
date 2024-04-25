using AutoMapper;
using FluentValidation;
using Moq;
using MusicShop.Infrastructure.Repository;
using MusicShop.Presentation.Common.DTOs.Category;
using MusicShop.Presentation.Controllers;
using MusicShop.Application.Services.ServiceHandler;
using Test.Presentation.Common;
namespace Test.Presentation.CategoryControllerTest
{

    public class TestCategoryController
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
        private readonly Mock<ICategoryServicesHandler> _mockService = new Mock<ICategoryServicesHandler>();
        private readonly Mock<IValidator<CategoryRequest>> _mockValidator = new Mock<IValidator<CategoryRequest>>();

        private readonly CategoryController _categoryController;
        public TestCategoryController()
        {
            _categoryController = new CategoryController(_unitOfWork.Object, _mockMapper.Object, _mockService.Object, _mockValidator.Object);
        }
        
        public async Task GetByIdAsync_ShouldReturnCATEGORY_WhenCategoryExist()
        {
        }
        public async Task GetListAsync_ShouldReturnCATEGORIES_WhenCategoriesExist()
        {
            
        }
    }
}

