using MusicShop.Infrastructure.Repository;
using Moq;
using MusicShop.Domain.Model.Core;
using System.Reflection.Metadata.Ecma335;
namespace MusicShop.UnitTests.Infrastructure.Repository.Mock.MockRepository
{
    public class MockCategoryRepository
    {
        public static Mock<ICategoryRepository> GetCategoryRepository()
        {
            var categories = new List<CategoryEntity>
            {
                new CategoryEntity
                {
                    Id=1,
                    Name="Category 1",
                    ChildCategories=new List<CategoryEntity>()
                    {
                        new CategoryEntity(),
                    },
                    Product=new List<ProductEntity>()
                    {
                        new ProductEntity()
                    }
                },
                new CategoryEntity
                {
                    Id=2,
                    Name="Category 2",
                    ChildCategories=new List<CategoryEntity>()
                    {
                        new CategoryEntity(),
                    },
                    Product=new List<ProductEntity>()
                    {
                        new ProductEntity()
                    }
                },
                new CategoryEntity
                {
                    Id=3,
                    Name="Category 3",
                    ChildCategories=new List<CategoryEntity>()
                    {
                        new CategoryEntity(),
                    },
                    Product=new List<ProductEntity>()
                    {
                        new ProductEntity()
                    }
                },


            };
            var categoryById = categories.FirstOrDefault(x => x.Id == 1);

            // generic repository methods
            var mockRepository = new Mock<ICategoryRepository>();
            mockRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(categories);
            mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(categoryById);

            mockRepository.Setup(x => x.Remove(It.IsAny<CategoryEntity>())).Callback(() =>
            {
                categories.Remove(categoryById);
            });
            mockRepository.Setup(x => x.Add(It.IsAny<CategoryEntity>())).Callback((CategoryEntity category) =>
            {
                categories.Add(category);
            });
            mockRepository.Setup(x => x.Update(It.IsAny<CategoryEntity>())).Callback((CategoryEntity category) =>
            {
                categoryById.Id = 4;
                categoryById.Name = "Category 4";
            });
            // unique method for repository

            mockRepository.Setup(x => x.GetCategoryWithChildren(It.IsAny<int>())).ReturnsAsync(categoryById);
            mockRepository.Setup(x => x.GetCategoryWithProducts(It.IsAny<int>())).ReturnsAsync(categoryById);
            return mockRepository;
        }
    }
}
