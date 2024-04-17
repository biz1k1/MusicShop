using AutoMapper;
using MusicShop.Application.Common.Models;
using MusicShop.Domain.Model.Aunth;
using MusicShop.Domain.Model.Core;
using MusicShop.Presentation.Common.DTOs.Authentication;
using MusicShop.Presentation.Common.DTOs.Category;
using MusicShop.Presentation.Common.DTOs.Product;
using MusicShop.Presentation.Common.DTOs.User;


namespace MusicShop.Application.Common.Mapping
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            // Category
            CreateMap<CategoryEntity, CategoryResponse>();

            CreateMap<CategoryResponse, CategoryEntity>();
            CreateMap<CategoryRequest, CategoryEntity>();

            CreateMap<CategoryEntity, CategoryResponseByProduct>()
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.products, src => src.MapFrom(x => x.Product));

            CreateMap<CategoryEntity, CategoryRequestUpdate>();

            CreateMap<CategoryRequestUpdate, CategoryEntity>();

            // Product
            CreateMap<ProductEntity, ProductRequest>();
            CreateMap<ProductRequest, ProductEntity>();

            CreateMap<ProductEntity, ProductResponse>()
                .ForMember(dest => dest.CategoryId, src => src.MapFrom(x => x.Category.Id));

            CreateMap<ProductRequestUpdate, ProductEntity>();
            //Authentication

            CreateMap<LoginRequest,LoginDTO>();
            CreateMap<LoginDTO, LoginRequest>();

            CreateMap<RegisterRequest, RegisterDTO>();
            CreateMap<RegisterDTO, RegisterRequest>();
            //Role
            CreateMap<UserRequest, RoleEntity>();
            //User
            CreateMap<UserEntity, UserResponse>();
        }


    }
}

