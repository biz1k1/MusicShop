using Microsoft.EntityFrameworkCore;
using Moq;
using MusicShop.Domain.Model.Core;
using MusicShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Test.Infrastructure.Common
{
    public class CategoryMockSet
    {
        public static Mock<DbSet<CategoryEntity>> MockSet(List<CategoryEntity> dataOrigin)
        {
            var data = dataOrigin.AsQueryable();
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
            return mockSet;
        }
    }
}
