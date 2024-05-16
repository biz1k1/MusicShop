using AutoMapper;
using Microsoft.Identity.Client;
using MusicShop.Domain.Model.Core;
using MusicShop.Presentation.Common.DTOs.Category;

namespace MusicShop.Tests.Helpers
{
    public class Helper
    {
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
        public static List<CategoryEntity> GetCategoryList()
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
    }
}
