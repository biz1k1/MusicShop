using Microsoft.EntityFrameworkCore;
using Moq;
using MusicShop.Domain.Model.Core;
using MusicShop.Infrastructure.Data;
using MusicShop.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Infrastructure.Common;

namespace Test.Infrastructure
{
    public class ProductRepositoryTests
    {
        [Fact]
        public async Task GetProductBlogsAsync()
        {

            var data = new List<ProductEntity>
            {
                new ProductEntity{ Name = "BBB" },
                new ProductEntity{ Name = "ZZZ" },
                new ProductEntity { Name = "AAA" },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ProductEntity>>();
            mockSet.As<IDbAsyncEnumerable<ProductEntity>>()
            .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<ProductEntity>(data.GetEnumerator()));
            mockSet.As<IQueryable<ProductEntity>>()
            .Setup(m => m.Provider)
            .Returns(new TestDbAsyncQueryProvider<ProductEntity>(data.Provider));
            mockSet.As<IQueryable<ProductEntity>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ProductEntity>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ProductEntity>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<DataContext>();
            mockContext.Setup(c => c.Products).Returns(mockSet.Object);

            var service = new ProductRepository(mockContext.Object);
            var categories = (await service.GetProductIncludeCategoryByIdAsync(1));

            Assert.Equal("AAA", categories.Name);
        }
    }
}
