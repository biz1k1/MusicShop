using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicShop.Domain.Model;
using MusicShop.Presentation.Common.DTOs.Category;

namespace MusicShop.UnitTests.Systems.Contollers.Categorys {
    class TestDataHelper {
    public static List<Category> GetFakeCategoryList() {
        return new List<Category>()
        {
        new Category
        {
            Id = 1,
            Name = "John Doe",
            
        },
        new Category
        {
            Id = 2,
            Name = "Mark Luther",
        }
    };
    }

    }
}
