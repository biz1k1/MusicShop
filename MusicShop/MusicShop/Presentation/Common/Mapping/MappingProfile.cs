using AutoMapper;
using MusicShop.Application.Services.Authentication;
using MusicShop.Domain.Model;
using MusicShop.Presentation.Common.DTOs.Authentication;
using MusicShop.Presentation.Common.DTOs.Category;
using MusicShop.Presentation.Common.DTOs.Product;
namespace MusicShop.Presentation.Common.Mapping
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            // Category
            CreateMap<Category, CategoryResponse>();
            CreateMap<CategoryResponse, Category>();
            CreateMap<CategoryRequest, Category>();

            CreateMap<Category, CategoryResponseByProduct>()
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.products, src => src.MapFrom(x => x.Product));

            CreateMap<Category, CategoryResponseUpdate>();

            // Product
            CreateMap<Product, ProductRequest>();
            CreateMap<ProductRequest, Product>();
            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.CategoryId, src => src.MapFrom(x => x.Category.Id));
            CreateMap<ProductRequestUpdate, Product>();
            //Authentication
            CreateMap<AuthenticationResult, AuthenticationResponse>();
            CreateMap< AuthenticationResponse, AuthenticationResult>();
        }


    }
}

