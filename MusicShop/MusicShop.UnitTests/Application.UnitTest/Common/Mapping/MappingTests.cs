using AutoMapper;
using MusicShop.Application;
using MusicShop.Application.Common.Models;
using MusicShop.Domain.Model.Aunth;
using MusicShop.Domain.Model.Core;
using MusicShop.Infrastructure.Data;
using MusicShop.Presentation.Common.DTOs.Authentication;
using MusicShop.Presentation.Common.DTOs.Category;
using MusicShop.Presentation.Common.DTOs.Product;
using MusicShop.Presentation.Common.DTOs.User;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MusicShop.UnitTests.Application.Common.Mapping
{
    public class MappingTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests()
        {
            _configuration = new MapperConfiguration(config =>
                config.AddMaps(Assembly.GetAssembly(typeof(DataContext))));

            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Theory]
        //Category
        [InlineData(typeof(CategoryEntity), typeof(CategoryRequest))]
        [InlineData(typeof(CategoryRequest), typeof(CategoryEntity))]
        [InlineData(typeof(CategoryEntity), typeof(CategoryRequestUpdate))]
        //Product
        [InlineData(typeof(ProductEntity), typeof(ProductRequest))]
        [InlineData(typeof(ProductEntity), typeof(ProductRequestUpdate))]
        [InlineData(typeof(ProductEntity), typeof(ProductResponse))]
        //User
        [InlineData(typeof(UserEntity), typeof(UserRequest))]
        [InlineData(typeof(UserEntity), typeof(UserResponse))]
        //Authentication
        [InlineData(typeof(LoginRequest), typeof(LoginDTO))]
        [InlineData(typeof(RegisterRequest), typeof(RegisterDTO))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            var instance = GetInstanceOf(source);

            _mapper.Map(instance, source, destination);
        }

        private object GetInstanceOf(Type type)
        {
            if (type.GetConstructor(Type.EmptyTypes) != null)
                return Activator.CreateInstance(type)!;

            // Type without parameterless constructor
            return RuntimeHelpers.GetUninitializedObject(type);
        }
    }
}
