using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MusicShop.Application.Common.Mapping;
using MusicShop.Application.Services.ServiceHandler;
using MusicShop.Domain.Model.Core;
using MusicShop.Infrastructure.Repository;
using MusicShop.Presentation.Common.DTOs.Category;
using MusicShop.Presentation.Controllers;
using MusicShop.Tests.Helpers;

namespace MusicShop.Tests.Presentation.UnitTest.CategoryControllerTests
{
    public class RepositoryCategoryControllerTests
    {
        private Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
        private Mock<ICategoryRepository> mockCategoryRepository = new Mock<ICategoryRepository>();
        private Mock<ICategoryServicesHandler> mockCategoryServiceHandler = new Mock<ICategoryServicesHandler>();
        private InlineValidator<CategoryRequest> mockValidator = new InlineValidator<CategoryRequest>();
        //for mapping
        private readonly IMapper _mapper;
        private readonly IConfigurationProvider _configuration;
        public RepositoryCategoryControllerTests()
        {
            _configuration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            _mapper = new Mapper(_configuration);
        }
        [Fact]
        public async Task GetCategories()
        {
            //arrange 
            var categories= Helper.GetCategoryList();

            mockCategoryServiceHandler.Setup(r => r.GetFullTreeCategories()).ReturnsAsync(categories);
            var controller = new CategoryController(mockUnitOfWork.Object,
                _mapper,
                mockCategoryServiceHandler.Object,
                mockValidator);

            //act 
            var result = controller.GetCategories().Result as ObjectResult;

            //assert 
            Assert.Equal(200,result.StatusCode);
            var model=Assert.IsType<List<CategoryResponse>>(result.Value);
            Assert.Equal(2, model.Count());
        }
        [Fact]
        public async Task GetCategoryById()
        {
            //arrange
            var category = Helper.GetCategory();

            mockCategoryRepository.Setup(x => x.GetCategoryWithChildren(It.IsAny<int>())).ReturnsAsync(category);
            mockUnitOfWork.Setup(x => x.Category).Returns(mockCategoryRepository.Object);
            var controller = new CategoryController(mockUnitOfWork.Object,
                _mapper,
                mockCategoryServiceHandler.Object,
                mockValidator);

            //act
            var result = await controller.GetCategoryById(1) as ObjectResult;

            //assert
            Assert.Equal(200, result.StatusCode);
            var model=Assert.IsType<CategoryResponse>(result.Value);
            Assert.Equal(1, model.Id);
        }
        [Fact]
        public async Task AddCategory()
        {
            //arrange
            var category = Helper.GetCategory();
            mockCategoryRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(category);
            mockUnitOfWork.Setup(x => x.Category).Returns(mockCategoryRepository.Object);
            var controller = new CategoryController(mockUnitOfWork.Object,
                _mapper,
                mockCategoryServiceHandler.Object,
                mockValidator);

            var newCategory = new CategoryRequest()
            {
                Name="category",
                SubCategoryId=0,
            };

            //act
            var result = await controller.AddCategory(newCategory) as ObjectResult;

            //assert
            //доделать
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<CategoryEntity>(result.Value);
            mockCategoryRepository.Verify();
        }
    }
}
