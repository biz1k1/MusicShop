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
            CreateMap<CategoryEntity, CategoryResponse>().ReverseMap();
            CreateMap<CategoryRequest, CategoryEntity>()
                .ForMember(x => x.Product, opt => opt.Ignore())
                .ForMember(x => x.ChildCategories, opt => opt.Ignore())
                .ForMember(x => x.ParentCategory, opt => opt.Ignore())
                .ForMember(x => x.ParentCategoryId, opt => opt.Ignore())
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<CategoryEntity, CategoryRequest>()
                .ForMember(x => x.SubCategoryId, opt => opt.Ignore());

            CreateMap<CategoryEntity, CategoryResponseByProduct>()
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.products, src => src.MapFrom(x => x.Product));

            CreateMap<CategoryEntity, CategoryRequestUpdate>()
                .ForMember(x => x.CategoryToChangeId, prop => prop.Ignore())
                .ReverseMap();
                


            // Product
            CreateMap<ProductEntity, ProductRequest>().ReverseMap();

            CreateMap<ProductEntity, ProductResponse>()
                .ForMember(dest => dest.CategoryId, src => src.MapFrom(x => x.Category.Id));

            CreateMap<ProductRequestUpdate, ProductEntity>()
                .ForMember(x=>x.Category,prop=>prop.Ignore());
            //Authentication

            CreateMap<LoginRequest, LoginDTO>().ReverseMap();

            CreateMap<RegisterRequest, RegisterDTO>().ReverseMap();
          
            //User
            CreateMap<UserEntity, UserResponse>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.Login, src => src.MapFrom(x => x.Login))
                .ForMember(dest => dest.Email, src => src.MapFrom(x => x.Email))
                .ForMember(dest => dest.Password, src => src.MapFrom(x => x.Password))
                .ForMember(dest => dest.Role, src => src.MapFrom(x => x.Roles.Select(x => x.Name).FirstOrDefault())).ReverseMap();
            CreateMap<UserEntity, UserRequest>().ReverseMap();
        }

    }
}


