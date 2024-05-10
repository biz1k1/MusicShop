using Moq;
using MusicShop.Application.Services.FullTreeCategories;
using MusicShop.Domain.Model.Core;
using MusicShop.UnitTests.Infrastructure.Repository.Mock.MockFullTreeCategories;

namespace MusicShop.UnitTests.Application.Services.FullTreeCategories
{
    public class TreeTest
    {
        private Mock<IFullTreeCategoryService> mockTreeService= new();
        [Fact]
        public void Should_return_success_when_get_list_TreeCategories()
        {
            mockTreeService = MockFullTreeCategories.GetTree();

            var mockFullTreeCategory = mockTreeService.Object.CheckIfTheAreChildrenAndAddThem(3, new List<CategoryEntity>());
            var category1 = mockFullTreeCategory.FirstOrDefault(x => x.Id == 1);
            var category2 = mockFullTreeCategory.FirstOrDefault(x => x.Id == 2);
            var category3 = mockFullTreeCategory.FirstOrDefault(x => x.Id == 3);

            Assert.Equal(2,category1.ChildCategories.First().Id);
            Assert.Equal(3,category2.ChildCategories.First().Id);
            Assert.Null(category3.ChildCategories.FirstOrDefault());

        }
    }
}
