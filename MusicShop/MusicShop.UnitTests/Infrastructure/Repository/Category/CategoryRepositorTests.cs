using Moq;
using MusicShop.Domain.Model.Core;
using MusicShop.Infrastructure.Repository;
using MusicShop.UnitTests.Infrastructure.Repository.MockRepository;

namespace MusicShop.UnitTests.Infrastructure.Repository.Category
{
    public class CategoryRepositoryCommandTests
    {
        private Mock<IUnitOfWork> _mockRepository;
        [Fact]
        public async Task Should_return_success_when_Get_Exist_CategoriesList()
        {
            //arrange
            _mockRepository = MockUnitOfWork.GetUnitOfWork();
            //act
            var result = await _mockRepository.Object.Category.GetAllAsync();
            //assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
        }
        [Fact]
        public async Task Should_return_success_when_Get_Exist_CategoryById()
        {
            //arrange
            _mockRepository = MockUnitOfWork.GetUnitOfWork();
            //act
            var result = await _mockRepository.Object.Category.GetByIdAsync(1);
            //assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }
        [Fact]
        public async Task Should_return_success_when_Add_Category()
        {
            //arrange
            _mockRepository = MockUnitOfWork.GetUnitOfWork();
             var categoryList = await _mockRepository.Object.Category.GetAllAsync();
            //act
            _mockRepository.Object.Category.Add(new CategoryEntity());
            //assert
            Assert.Equal(4, categoryList.Count());

        }
        [Fact]
        public async Task Should_return_success_when_Remove_Category()
        {
            //arrange
            _mockRepository = MockUnitOfWork.GetUnitOfWork();
            var categoryList = await _mockRepository.Object.Category.GetAllAsync();
            //act
            _mockRepository.Object.Category.Remove(new CategoryEntity());
            //assert
            Assert.Equal(2, categoryList.Count());
        }
        [Fact]
        public async Task Should_return_success_when_Update_Category()
        {
            //arrange
            _mockRepository = MockUnitOfWork.GetUnitOfWork();
            var category = await _mockRepository.Object.Category.GetByIdAsync(1);
            //act 
            _mockRepository.Object.Category.Update(new CategoryEntity());
            //assert
            Assert.Equal(category.Id, 4);
        }
    }
}
