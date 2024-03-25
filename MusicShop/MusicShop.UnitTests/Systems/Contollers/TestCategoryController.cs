using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using MusicShop.Infrastructure.Data;
using AutoMapper;
using MusicShop.Presentation.Controllers;
using Moq;
using MusicShop.Application.Services.ServiceHandler;
using FluentValidation;
using MusicShop.Presentation.Common.DTOs.Category;
using Microsoft.EntityFrameworkCore;
using MusicShop.Domain.Model;
using Assert = Xunit.Assert;

namespace MusicShop.UnitTests.Systems.Contollers
{
    public class TestCategoryController {
        private CategoryController _categoryController;
        private readonly Mock<DbSet<Category>> _mockSet =new Mock<DbSet<Category>>();
        private readonly Mock<DataContext>_mockContext= new Mock<DataContext>();
        
        private readonly Mock<IMapper> _mockMapper=new Mock<IMapper>();
        private readonly Mock<ICategoryServicesHandler> _mockService = new Mock<ICategoryServicesHandler>();
        private readonly Mock<IValidator<CategoryRequest>> _mockValidator= new Mock<IValidator<CategoryRequest>>();
        public TestCategoryController()
        {
            _mockContext.Setup(x => x.Categories).Returns(_mockSet.Object);
            //_categoryController = new CategoryController(_mockContext.Object, _mockMapper.Object, _mockService.Object, _mockValidator.Object);
        }
        [Fact]
        public async Task GetByIdAsync_ShouldReturnCATEGORY_WhenCategoryExist() {
            //Arrange
            _mockContext.Setup(x=>x.Categories).Returns(_mockSet.Object);
            //Act
            var categories =  _categoryController.GetCategoryById(1);
            //Assert
            Assert.Equal(1,categories.Id);
        }
        public async Task GetListAsync_ShouldReturnCATEGORIES_WhenCategoriesExist()
        {
            //Arrange
            _mockContext.Setup(x => x.Categories).Returns(_mockSet.Object);
            //Act
            var categories = _categoryController.GetCategories();
            //Assert

            //Assert.Equal();
        }
        
    }
}