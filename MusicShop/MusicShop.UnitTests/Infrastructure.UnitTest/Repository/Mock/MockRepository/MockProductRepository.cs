using Moq;
using MusicShop.Domain.Model.Core;
using MusicShop.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.UnitTests.Infrastructure.Repository.Mock.MockRepository
{
    public class MockProductRepository
    {
        public static Mock<IProductRepository> GetProductRepository()
        {
            var products = new List<ProductEntity>
            {
                new ProductEntity
                {
                    Id=1,
                    Name="Product 1",
                },
                new ProductEntity
                {
                    Id=2,
                    Name="Product 2",
                },
                new ProductEntity
                {
                    Id=3,
                    Name="Product 3",
                },


            };
            var productById = products.FirstOrDefault(x => x.Id == 1);

            // generic repository methods
            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(products);
            mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(productById);
            mockRepository.Setup(x => x.Remove(It.IsAny<ProductEntity>())).Callback(() =>
            {
                products.Remove(productById);
                return;
            });
            mockRepository.Setup(x => x.Add(It.IsAny<ProductEntity>())).Callback((ProductEntity product) =>
            {
                products.Add(product);
                return;
            });
            mockRepository.Setup(x => x.Update(It.IsAny<ProductEntity>())).Callback((ProductEntity category) =>
            {
                productById.Id = 4;
                productById.Name = "Category 4";
                return;
            });
            // unique method for repository
            mockRepository.Setup(x => x.GetProductIncludeCategoryByIdAsync(It.IsAny<int>())).ReturnsAsync(productById);
            mockRepository.Setup(x => x.GetProductsIncludeCategoryAsync()).ReturnsAsync(products);

            return mockRepository;
        }
    }
}
