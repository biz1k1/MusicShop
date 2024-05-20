using AutoMapper;
using Microsoft.Identity.Client;
using MusicShop.Domain.Model.Core;
using MusicShop.Presentation.Common.DTOs.Category;

namespace MusicShop.Tests.Helpers
{
    public class Helper
    {
        #region Category setter

        public static CategoryEntity GetCategory()
        {
            var category = new CategoryEntity()
            {
                Id = 1,
                Name="Category 1",
                ChildCategories = new List<CategoryEntity>()
                {
                    new CategoryEntity()
                    {
                        Id=2,
                        Name="Category 2",
                    }
                }
            };
            return category;
        }
        public static List<CategoryEntity> GetCategoriesList()
        {
            var categories = new List<CategoryEntity>
            {
                new CategoryEntity
                {
                    Id=1,
                    Name="Category 1",
                    Product=new List<ProductEntity>()
                    {
                        new ProductEntity()
                    }
                },
                new CategoryEntity
                {
                    Id=2,
                    Name="Category 2",
                    Product=new List<ProductEntity>()
                    {
                        new ProductEntity()
                    }
                },


            };
            return categories;
           
        }
        #endregion

        #region Product setter
        public static List<ProductEntity> GetProductsList()
        {
            var products = new List<ProductEntity>()
            {
                new ProductEntity()
                {
                    Id=1,
                    Name="Product",
                },
                new ProductEntity()
                {
                    Id=2,
                    Name="Product 2",
                }
            };
            return products;
        }
        public static ProductEntity GetProduct()
        {
            var product = new ProductEntity()
            {
                Id = 1,
                Name = "Product 1",
            };
            return product;
        }
        #endregion
    }
}
