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
using MusicShop.UnitTests.Systems.Contollers.Categorys;

namespace MusicShop.UnitTests.Systems.Contollers
{
    public class TestCategoryController {
        private readonly CategoryController _categoryController;
        private readonly Mock<DataContext> _dbMock=new Mock<DataContext>();
        
        private readonly Mock<IMapper> _mapperMock=new Mock<IMapper>();
        private readonly Mock<ICategoryServicesHandler> _IServiceMock = new Mock<ICategoryServicesHandler>();
        private readonly Mock<IValidator<CategoryRequest>> _validator= new Mock<IValidator<CategoryRequest>>();
        TestCategoryController() { 

        }
        [Fact]
        public async Task GetByIdAsync_ShouldReturnCATEGORY_WhenCATEGORYExist() {
            var categoryId = 1;
            var category = (await _categoryController.GetCategories());
            
            //Assert.Equals(2,_dbMock.);
        }
        
    }
}