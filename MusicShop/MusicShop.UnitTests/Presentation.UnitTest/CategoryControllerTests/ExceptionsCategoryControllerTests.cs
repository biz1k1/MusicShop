using AutoMapper;
using FluentResults;
using FluentValidation;
using Moq;
using MusicShop.Application.Common.Errors;
using MusicShop.Application.Services.ServiceHandler;
using MusicShop.Domain.Model.Core;
using MusicShop.Infrastructure.Repository;
using MusicShop.Presentation.Common.DTOs.Category;
using MusicShop.Presentation.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Tests.Presentation.UnitTest.CategoryControllerTest
{
    public class ExceptionsCategoryControllerTests
    {
        CategoryController sut;
        Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
        Mock<ICategoryRepository> mockCategoryRepository = new Mock<ICategoryRepository>();
        Mock<ICategoryServicesHandler> mockCategoryServiceHandler = new Mock<ICategoryServicesHandler>();
        Mock<IMapper> mockMapper = new Mock<IMapper>();
        InlineValidator<CategoryRequest> mockValidator = new InlineValidator<CategoryRequest>();

        const int Id_Of_Non_Existent_Category = 100;
        public ExceptionsCategoryControllerTests()
        {
            sut = new CategoryController(mockUnitOfWork.Object,
                mockMapper.Object,
                mockCategoryServiceHandler.Object,
                mockValidator);
        }

        [Fact]
        public async Task GetCategoryById_throw_CategoryNotFound_exception_when_CategoryObject_NULL()
        {
            //arrange
            mockUnitOfWork.Setup(x => x.Category).Returns(mockCategoryRepository.Object);
            //act
            var result = sut.GetCategoryById(Id_Of_Non_Existent_Category);

            //assert
            await Assert.ThrowsAsync<CategoryNotFound>(() => result);

        }
        [Fact]
        public async Task Delete_throw_CategoryNotFound_exception__when_CategoryObject_NULL()
        {
            //arrange
            mockUnitOfWork.Setup(x => x.Category).Returns(mockCategoryRepository.Object);
            //act
            var result = sut.DeleteCategory(Id_Of_Non_Existent_Category);

            //assert
            await Assert.ThrowsAsync<CategoryNotFound>(() => result);

        }

        [Fact]
        public async Task Update_throw_CategoryNotFound_exception_when_CategoryObject_NULL()
        {
            //arrange
            const int sameIntNumberForException = 10;
            mockCategoryRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() =>
            new CategoryEntity
            {
                Id = sameIntNumberForException
            });
            mockUnitOfWork.Setup(x => x.Category).Returns(mockCategoryRepository.Object);

            //act
            var result = sut.UpdateCategory(new CategoryRequestUpdate());

            //assert
            await Assert.ThrowsAsync<CategoryReference>(() => result);

        }
    }
}
