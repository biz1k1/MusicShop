using Moq;
using MusicShop.Domain.Model.Core;
using System.Data.Entity.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MusicShop.Infrastructure.Data;
using MusicShop.Infrastructure.Repository;
using Test.Infrastructure.Common;
using System.Reflection.Metadata;
namespace Test.Infrastructure
{
    public class CategoryRepositoryTests
    {
        [Fact]
        public async Task GetAllCategoriesAsync()
        {

            var data = new List<CategoryEntity>
            {
                new CategoryEntity { Name = "BBB" },
                new CategoryEntity { Name = "ZZZ" },
                new CategoryEntity { Name = "AAA" },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<CategoryEntity>>();
            mockSet.As<IDbAsyncEnumerable<CategoryEntity>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<CategoryEntity>(data.GetEnumerator()));

            mockSet.As<IQueryable<CategoryEntity>>()
            .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<CategoryEntity>(data.Provider));

            mockSet.As<IQueryable<CategoryEntity>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<CategoryEntity>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<CategoryEntity>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<DataContext>();
            mockContext.Setup(c => c.Categories).Returns(mockSet.Object);





            var service = new CategoryRepository(mockContext.Object);
            var categories = (await service.GetAllAsync()).ToList();

            Assert.Equal(3, categories.Count());
            Assert.Equal("AAA", categories[0].Name);
            Assert.Equal("BBB", categories[1].Name);
            Assert.Equal("ZZZ", categories[2].Name);
        }

        
        public async Task GetByIdAsync()
        {

        }
    }
}
