using Moq;
using MusicShop.Application.Services.FullTreeCategories;
using MusicShop.Application.Services.ServiceHandler;
using MusicShop.Domain.Model.Core;
using MusicShop.Infrastructure.Repository;
using MusicShop.Tests.Helpers;
using System.Linq;

namespace MusicShop.UnitTests.Application.Services.FullTreeCategories
{
    public class CategoryServicesHandlerTests
    {
        private readonly Mock<IUnitOfWork> mockUnitOfWork;
        private readonly Mock<IFullTreeCategoryService> mockTree = new Mock<IFullTreeCategoryService>();
        private readonly Mock<ICategoryRepository> mockCategoryRepository;
        private readonly CategoryServicesHandler _handler;
        public CategoryServicesHandlerTests()
        {
            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockTree=new Mock<IFullTreeCategoryService>();
            mockCategoryRepository = new Mock<ICategoryRepository>();
            _handler=new CategoryServicesHandler(mockTree.Object, mockUnitOfWork.Object);

        }
        [Fact]
        public async Task Should_return_success_when_method_GetFullTreeCategories_return_allCategories()
        {
            //assert
            mockCategoryRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(Helper.GetCategoryList);
            mockUnitOfWork.Setup(x => x.Category).Returns(mockCategoryRepository.Object);
            mockTree.Setup(x => x.CheckIfTheAreChildrenAndAddThem(It.IsAny<int>(), It.IsAny<IEnumerable<CategoryEntity>>())).Returns(Helper.GetCategoryList);

            //act
            var allCategoriers = (await _handler.GetFullTreeCategories()).ToList();

            //assert

            Assert.NotNull(allCategoriers);
           //Assert.Equal(3, allCategoriers.Count);
            Assert.Equal("Category 1", allCategoriers[0].Name);
            Assert.Equal("Category 2", allCategoriers[1].Name);
            Assert.Equal("Category 2", allCategoriers[2].Name);
        }
    }
}
