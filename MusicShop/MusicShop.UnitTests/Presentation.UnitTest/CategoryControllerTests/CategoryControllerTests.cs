using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using MusicShop.Application.Common.Mapping;
using MusicShop.Application.Services.ServiceHandler;
using MusicShop.Domain.Model.Core;
using MusicShop.Infrastructure.Repository;
using MusicShop.Presentation.Common.DTOs.Category;
using MusicShop.Presentation.Controllers;
using MusicShop.Tests.Helpers;
using System.Net;


namespace MusicShop.Tests.Presentation.UnitTest.CategoryControllerTests
{
    public class CategoryControllerTests
    {
        private Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
        private Mock<ICategoryRepository> mockCategoryRepository = new Mock<ICategoryRepository>();
        private Mock<ICategoryServicesHandler> mockCategoryServiceHandler = new Mock<ICategoryServicesHandler>();
        private InlineValidator<CategoryRequest> mockValidator = new InlineValidator<CategoryRequest>();
        //for mapping
        private readonly IMapper _mapper;
        private readonly IConfigurationProvider _configuration;
        public CategoryControllerTests()
        {
            _configuration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            _mapper = new Mapper(_configuration);
        }
        public async Task GetCategories_return_CategoriesList()
        {
            //arrange 

            mockCategoryServiceHandler.Setup(r => r.GetFullTreeCategories()).ReturnsAsync(Helper.GetCategoriesList());
            var controller = new CategoryController(mockUnitOfWork.Object,
                _mapper,
                mockCategoryServiceHandler.Object,
                mockValidator);

            //act 
            var result = controller.GetCategories().Result as ObjectResult;

            //assert 
            var model=Assert.IsType<List<CategoryResponse>>(result.Value);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task GetCategoryById()
        {
            //arrange
            int testIdCategory = 1;

            mockCategoryRepository.Setup(x => x.GetCategoryWithChildren(It.IsAny<int>())).ReturnsAsync(Helper.GetCategory());
            mockUnitOfWork.Setup(x => x.Category).Returns(mockCategoryRepository.Object);
            var controller = new CategoryController(mockUnitOfWork.Object,
                _mapper,
                mockCategoryServiceHandler.Object,
                mockValidator);

            //act
            var result = await controller.GetCategoryById(testIdCategory) as ObjectResult;

            //assert
            var model=Assert.IsType<CategoryResponse>(result.Value);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(testIdCategory, model.Id);
        }
        [Fact]
        public async Task AddCategory()
        {
            //arrange
            string testName = "Category 1";
            int testId = 1;

            mockCategoryRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(Helper.GetCategory());
            mockUnitOfWork.Setup(x => x.Category).Returns(mockCategoryRepository.Object);
            var controller = new CategoryController(mockUnitOfWork.Object,
                _mapper,
                mockCategoryServiceHandler.Object,
                mockValidator);

            var newCategory = new CategoryRequest()
            {
                Name= testName,
                SubCategoryId=testId,
            };

            //act
            var result = await controller.AddCategory(newCategory) as ObjectResult;

            //assert
            var returnCategory=Assert.IsType<CategoryEntity>(result.Value);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(testName,returnCategory.Name);
            // SubcategoryID не мапится в объекте CategoryEntity
            // (он нужен для поиска категории, к которой будет добвлена новая категория)
            // , а автоинкремент работает только в бд,из-за этого
            // ID в возращаемом объекте в юнит тестах будет 0.
            Assert.Equal(0,returnCategory.Id);
        }
        [Fact]
        public async Task DeleteCategory()
        {
            //arrange
            int testIdCategory = 1;
            mockCategoryRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(Helper.GetCategory());
            mockUnitOfWork.Setup(x => x.Category).Returns(mockCategoryRepository.Object);

            var controller = new CategoryController(mockUnitOfWork.Object,
               _mapper,
               mockCategoryServiceHandler.Object,
               mockValidator);
            //act
            var result = await controller.DeleteCategory(testIdCategory);
            var statusCodeResult = ((IStatusCodeActionResult)result).StatusCode;
            //assert
            Assert.Equal((int)HttpStatusCode.OK, statusCodeResult);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task UpdateCategory()
        {
            //arrange
            int testIdCategory = 1;
            string testName = "Category";
            int testParentCategory = 2;

            mockCategoryRepository.SetupSequence(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Helper.GetCategory())
                .ReturnsAsync(Helper.GetCategory().ChildCategories.FirstOrDefault());
            mockUnitOfWork.Setup(x => x.Category).Returns(mockCategoryRepository.Object);

            var categoryUpdate = new CategoryRequestUpdate()
            {
                CategoryToChangeId = testIdCategory,
                Name = testName,
                ParentCategoryId = testParentCategory,

            };

            var controller = new CategoryController(mockUnitOfWork.Object,
              _mapper,
              mockCategoryServiceHandler.Object,
              mockValidator);
            //act 
            var result = await controller.UpdateCategory(categoryUpdate);
            var statusCodeResult = ((IStatusCodeActionResult)result).StatusCode;
            //assert
            Assert.Equal((int)HttpStatusCode.OK, statusCodeResult);
            Assert.IsType<OkResult>(result);

        }
    }
}
