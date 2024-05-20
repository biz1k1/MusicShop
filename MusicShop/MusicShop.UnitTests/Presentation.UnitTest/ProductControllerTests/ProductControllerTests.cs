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
using MusicShop.Presentation.Common.DTOs.Product;
using MusicShop.Presentation.Controllers;
using MusicShop.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Tests.Presentation.UnitTest.ProductControllerTests
{
    public  class ProductControllerTests
    {
        private Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
        private Mock<ICategoryRepository> mockCategoryRepository = new Mock<ICategoryRepository>();
        private Mock<IProductRepository> mockProductRepository = new Mock<IProductRepository>();
        private InlineValidator<ProductRequest> mockValidator = new InlineValidator<ProductRequest>();
        //for mapping
        private readonly IMapper _mapper;
        private readonly IConfigurationProvider _configuration;
        public ProductControllerTests()
        {
            _configuration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            _mapper = new Mapper(_configuration);
        }
        [Fact]
        public async Task GetProductByCategory()
        {
            //arrange
            int testIdProduct = 1;

            mockCategoryRepository.Setup(x => x.GetCategoryWithProducts(It.IsAny<int>())).ReturnsAsync(Helper.GetCategory());
            mockUnitOfWork.Setup(x => x.Category).Returns(mockCategoryRepository.Object);
            var controller = new ProductController(
                mockUnitOfWork.Object,
                _mapper,
                mockValidator);

            //act
            var result = await controller.GetProductByCategory(testIdProduct) as ObjectResult;

            //assert
            var model = Assert.IsType<CategoryResponseByProduct>(result.Value);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(testIdProduct, model.Id);
        }
        [Fact]
        public async Task GetAllProduct()
        {
            //arrange 

            mockProductRepository.Setup(r => r.GetProductsIncludeCategoryAsync()).ReturnsAsync(Helper.GetProductsList());
            mockUnitOfWork.Setup(x => x.Product).Returns(mockProductRepository.Object);
            var controller = new ProductController(
                 mockUnitOfWork.Object,
                _mapper,
                mockValidator);

            //act 
            var result = await controller.GetAllProduct() as ObjectResult;

            //assert 
            var model = Assert.IsType<List<ProductResponse>>(result.Value);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(2, model.Count());
        }
        [Fact]
        public async Task GetProductById()
        {
            //arrange
            int testIdProduct = 1;

            mockProductRepository.Setup(x => x.GetProductIncludeCategoryByIdAsync(It.IsAny<int>())).ReturnsAsync(Helper.GetProduct());
            mockUnitOfWork.Setup(x => x.Product).Returns(mockProductRepository.Object);
            var controller = new ProductController(
                mockUnitOfWork.Object,
                _mapper,
                mockValidator);

            //act
            var result = await controller.GetProductById(testIdProduct) as ObjectResult;

            //assert
            var model = Assert.IsType<ProductResponse>(result.Value);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(testIdProduct, model.Id);
        }
        [Fact]
        public async Task CreateProduct()
        {
            //arrange
            string testNameProduct = "product 1";
            int testIdProduct = 0;
            int testIdCategory = 1;

            mockCategoryRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(Helper.GetCategory());
            mockUnitOfWork.Setup(x => x.Category).Returns(mockCategoryRepository.Object);
            mockUnitOfWork.Setup(x => x.Product).Returns(mockProductRepository.Object);
            var controller = new ProductController(
                 mockUnitOfWork.Object,
                _mapper,
                mockValidator); ;

            var newProduct = new ProductRequest()
            {
                Name = testNameProduct,
                CategoryId=testIdProduct
            };

            //act
            var result = await controller.CreateProduct(newProduct) as ObjectResult;

            //assert
            var returnProduct = Assert.IsType<ProductEntity>(result.Value);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);

            Assert.Equal(testNameProduct, returnProduct.Name);
            // returnProduct.Id не мапится в объекте типа ProductEntity
            // , а автоинкремент работает только в бд,из-за этого
            // ID в возращаемом объекте в юнит тестах будет 0.
            Assert.Equal(testIdProduct, returnProduct.Id);
            Assert.Equal(testIdCategory, returnProduct.Category.Id);
        }
        [Fact]
        public async Task DeleteProduct()
        {
            //arrange
            int testIdCategory = 1;
            mockProductRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(Helper.GetProduct());
            mockUnitOfWork.Setup(x => x.Product).Returns(mockProductRepository.Object);

            var controller = new ProductController(
                mockUnitOfWork.Object,
                _mapper,
                mockValidator);
            //act
            var result = await controller.DeleteProduct(testIdCategory);
            var statusCodeResult = ((IStatusCodeActionResult)result).StatusCode;
            //assert
            Assert.Equal((int)HttpStatusCode.OK, statusCodeResult);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task UpdateProduct()
        {
            //arrange
            string testNameProduct = "product 1";
            int testIdProduct = 1;
            int testIdCategory = 1;

            mockCategoryRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(Helper.GetCategory());
            mockProductRepository.Setup(x => x.GetProductIncludeCategoryByIdAsync(It.IsAny<int>())).ReturnsAsync(Helper.GetProduct());
            mockUnitOfWork.Setup(x => x.Category).Returns(mockCategoryRepository.Object);
            mockUnitOfWork.Setup(x => x.Product).Returns(mockProductRepository.Object);

            var productUpdate = new ProductRequestUpdate()
            {
                Id= testIdProduct,
                CategoryId=testIdCategory,
                Name=testNameProduct,

            };

            var controller = new ProductController(
               mockUnitOfWork.Object,
                _mapper,
                mockValidator);
            //act 
            var result = await controller.UpdateProduct(productUpdate);
            var statusCodeResult = ((IStatusCodeActionResult)result).StatusCode;
            //assert
            Assert.Equal((int)HttpStatusCode.OK, statusCodeResult);
            Assert.IsType<OkResult>(result);

        }

    }
}
