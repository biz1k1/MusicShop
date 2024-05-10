using FluentValidation.Validators;
using Moq;
using MusicShop.Application.Services.FullTreeCategories;
using MusicShop.Domain.Model.Core;

namespace MusicShop.UnitTests.Infrastructure.Repository.Mock.MockFullTreeCategories
{
    public class MockFullTreeCategories
    {
        public static Mock<IFullTreeCategoryService> GetTree()
        {
            Mock<IFullTreeCategoryService> mock = new Mock<IFullTreeCategoryService>();
            IEnumerable<CategoryEntity> category3 = new List<CategoryEntity>
            {
                new CategoryEntity
                {
                Id = 3,
                Name = "Category 3",
                ParentCategoryId=2,
                }
            };
            IEnumerable<CategoryEntity> category2 = new List<CategoryEntity>
            {
                 new CategoryEntity
                {
                Id = 2,
                Name = "Category 2",
                ParentCategoryId=1,
                ChildCategories= (ICollection<CategoryEntity>)category3,
                }
            };
            IEnumerable<CategoryEntity> category1 = new List<CategoryEntity>
            {
                new CategoryEntity
                {
                Id = 1,
                Name = "Category 1",
                ParentCategoryId=0,
                ChildCategories = (ICollection<CategoryEntity>)category2
                }
            };
            var children = new List<CategoryEntity>();
            children.Add(category1.First());
            children.Add(category2.First());
            children.Add(category3.First());

            mock.Setup(x => x.CheckIfTheAreChildrenAndAddThem(It.IsAny<int>(), It.IsAny<IEnumerable<CategoryEntity>>())).Returns(children);
            return mock;

        }
    }
}
