using Moq;
using MusicShop.Infrastructure.Repository;
namespace MusicShop.UnitTests.Infrastructure.Repository.MockRepository
{
    public class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> GetUnitOfWork()
        {
            var mockRepository = new Mock<IUnitOfWork>();
            var mockCategoryRepository = MockCategoryRepository.GetCategoryRepository();
            mockRepository.Setup(x => x.Category).Returns(mockCategoryRepository.Object);
            return mockRepository;
        }

    }
}
