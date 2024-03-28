using AutoMapper;
using Moq;
using MusicShop.Application.Services.ServiceHandler;
using FluentValidation;
using MusicShop.Infrastructure.Repository;
using MusicShop.Presentation.Common.DTOs.Category;
using MusicShop.Presentation.Controllers;

namespace Test.CategoryControllerTest
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
            [Fact]
            public async Task GetByIdAsync_ShouldReturnCATEGORY_WhenCategoryExist()
            {
                //Arrange
                var categoryId = 1;
                _unitOfWork.Setup(x => x.Category.FindByCondition(x => x.Id == categoryId).Returns(_unitOfWork.Object));
                //Act
                var category = await _categoryController.GetCategoryById(categoryId);
                //Assert
                Assert.Equal(1, category.Id);
            }
            public async Task GetListAsync_ShouldReturnCATEGORIES_WhenCategoriesExist()
            {
                //Arrange

                //Act

                //Assert

                //Assert.Equal();
            }
        }
    }

    