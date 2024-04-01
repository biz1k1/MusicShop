using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MusicShop.Application.Services.ServiceHandler;
using MusicShop.Infrastructure.Repository;
using MusicShop.Presentation.Common.DTOs.Category;
using MusicShop.Presentation.Controllers;
using AutoFixture;
using MusicShop.Domain.Model;
namespace Test.CategoryControllerTest
{

    public class TestCategoryController
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
        private readonly Mock<ICategoryServicesHandler> _mockService = new Mock<ICategoryServicesHandler>();
        private readonly Mock<IValidator<CategoryRequest>> _mockValidator = new Mock<IValidator<CategoryRequest>>();

        private readonly CategoryController _categoryController;
        private readonly Fixture _fixture;
        public TestCategoryController()
        {
            _categoryController = new CategoryController(_unitOfWork.Object, _mockMapper.Object, _mockService.Object, _mockValidator.Object);
            _fixture = new Fixture();
        }
        [Fact]
        public async Task GetByIdAsync_ShouldReturnCATEGORY_WhenCategoryExist()
        {
            //Arrange
            var categoryList = _fixture.CreateMany<Category>().ToList();
            //_unitOfWork.Setup(x => x.Category.FindByCondition(x => x.Id == 1)).Returns(categoryList);
            //Act
            var category = await _categoryController.GetCategoryById(1);
            var okResult =  category as ObjectResult;
            //Assert
            Assert.NotNull(category);
            Assert.Equal(200, okResult.StatusCode);
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

