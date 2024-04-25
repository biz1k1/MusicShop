using MusicShop.Domain.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Presentation.Common
{
    internal class CategoryList
    {
        private static List<CategoryEntity> categories;
        public static List<CategoryEntity> Categories()
        {
            categories = new List<CategoryEntity>()
            {
                new CategoryEntity{Id=1,Name="category1"},
                new CategoryEntity{ Id=2,Name="category2"}
            };
            return categories;
        }
    }
}
