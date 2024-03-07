using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using MusicShop.Infrastructure.Data;
using AutoMapper;
using MusicShop.Presentation.Controllers;

namespace MusicShop.UnitTests.Systems.Contollers
{
    public class TestCategoryController {
        private CategoryController systemTest;
        private DataContext db;
        private IMapper mapper;
        [SetUp]
        public void Setup() {
           // var systemTest = new CategoryController(db, mapper);
        }
        
    }
}