using MusicShop.Infrastructure.Repository;
using Moq;
using MusicShop.Domain.Model.Core;
namespace MusicShop.UnitTests.Infrastructure.Repository.MockRepository
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
                },
                new CategoryEntity
                {
                    Id=2,
                    Name="Category 2",
                },
                new CategoryEntity
                {
                    Id=3,
                    Name="Category 3",
                },


            };
            var categoryById = categories.FirstOrDefault(x => x.Id == 1);


            var mockRepository = new Mock<ICategoryRepository>();
            mockRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(categories);
            mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(categoryById);
            mockRepository.Setup(x => x.Remove(It.IsAny<CategoryEntity>())).Callback(() =>
            {
                categories.Remove(categoryById);
                return;
            });
            mockRepository.Setup(x => x.Add(It.IsAny<CategoryEntity>())).Callback((CategoryEntity category) =>
            {
                categories.Add(category);
                return;
            });
            mockRepository.Setup(x => x.Update(It.IsAny<CategoryEntity>())).Callback((CategoryEntity category) =>
            {
                categoryById.Id = 4;
                categoryById.Name = "Category 4";
            });
            return mockRepository;
        }
    }
}
