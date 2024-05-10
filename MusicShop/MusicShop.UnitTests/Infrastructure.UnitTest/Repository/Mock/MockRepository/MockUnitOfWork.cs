using Moq;
using MusicShop.Infrastructure.Repository;
namespace MusicShop.UnitTests.Infrastructure.Repository.Mock.MockRepository
{
    public class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> GetUnitOfWork()
        {
            var mockRepository = new Mock<IUnitOfWork>();
            var mockCategoryRepository = MockCategoryRepository.GetCategoryRepository();
           var mockProductRepository = MockProductRepository.GetProductRepository();
            var mockUserRepository = MockUserRepository.GetUserRepository();


            mockRepository.Setup(x => x.Category).Returns(mockCategoryRepository.Object);
            mockRepository.Setup(x => x.Product).Returns(mockProductRepository.Object);
            mockRepository.Setup(x => x.User).Returns(mockUserRepository.Object);


            return mockRepository;
        }

    }
}
