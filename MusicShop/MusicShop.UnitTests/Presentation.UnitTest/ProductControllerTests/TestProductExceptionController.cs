using AutoMapper;
using FluentResults;
using FluentValidation;
using Moq;
using MusicShop.Application.Common.Errors;
using MusicShop.Application.Services.ServiceHandler;
using MusicShop.Domain.Model.Core;
using MusicShop.Infrastructure.Repository;
using MusicShop.Presentation.Common.DTOs.Category;
using MusicShop.Presentation.Common.DTOs.Product;
using MusicShop.Presentation.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Tests.Presentation.UnitTest.ProductControllerTests
{
    public  class TestProductExceptionController
    {
        ProductController sut;
        Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
        Mock<ICategoryRepository> mockCategoryRepository = new Mock<ICategoryRepository>();
        Mock<IProductRepository> mockProductRepository = new Mock<IProductRepository>();
        Mock<IMapper> mockMapper = new Mock<IMapper>();
        InlineValidator<ProductRequest> mockValidator = new InlineValidator<ProductRequest>();

        const int Id_Of_Non_Existent_Product= 10;
        public TestProductExceptionController()
        {
            sut = new ProductController(mockUnitOfWork.Object,mockMapper.Object,mockValidator);
        }
        [Fact]
        public async Task GetProductByCategory_throw_CategoryNotFound_exception_when_CategoryObject_NULL()
        {
            mockUnitOfWork.Setup(x => x.Category).Returns(mockCategoryRepository.Object);

            var result = sut.GetProductByCategory(Id_Of_Non_Existent_Product);

            await Assert.ThrowsAsync<CategoryNotFound>(() => result);
        }
        [Fact]
        public async Task GetProductById_throw_ProductNotFound_exception_when_ProductObject_NULL()
        {
            mockUnitOfWork.Setup(x => x.Product).Returns(mockProductRepository.Object);

            var result = sut.GetProductById(Id_Of_Non_Existent_Product);

            await Assert.ThrowsAsync<ProductNotFound>(() => result);
        }
        [Fact]
        public async Task Add_throw_CategoryNotFound_exception_when_Adding_Product_in_nonExistent()
        {
            mockUnitOfWork.Setup(x => x.Category).Returns(mockCategoryRepository.Object);
            mockValidator.Validate(new ProductRequest());

            var result = sut.Create(new ProductRequest());

            await Assert.ThrowsAsync<CategoryNotFound>(() => result);
        }
        [Fact]
        public async Task Delete_throw_ProductNotFound_exception_when_ProductObject_NULL()
        {
            mockUnitOfWork.Setup(x => x.Product).Returns(mockProductRepository.Object);

            var result = sut.Delete(Id_Of_Non_Existent_Product);

            await Assert.ThrowsAsync<ProductNotFound>(() => result);
        }
        [Fact]
        public async Task Update_throw_ProductNotFound_exception_when_ProductObject_NULL()
        {
            mockUnitOfWork.Setup(x => x.Product).Returns(mockProductRepository.Object);

            var result = sut.Update(new ProductRequestUpdate());

            await Assert.ThrowsAsync<ProductNotFound>(() => result);
        }
        [Fact]
        public async Task Update_throw_CategoryNotFound_exception_when_CategoryObject_NULL()
        {
            mockProductRepository.Setup(x => x.GetProductIncludeCategoryByIdAsync(It.IsAny<int>())).ReturnsAsync(new ProductEntity());
            mockUnitOfWork.Setup(x => x.Category).Returns(mockCategoryRepository.Object);
            mockUnitOfWork.Setup(x => x.Product).Returns(mockProductRepository.Object);

            var result = sut.Update(new ProductRequestUpdate());

            await Assert.ThrowsAsync<CategoryNotFound>(() => result);
        }

    }
}
