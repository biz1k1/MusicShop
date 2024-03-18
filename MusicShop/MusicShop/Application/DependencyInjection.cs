﻿using Microsoft.EntityFrameworkCore;
using MusicShop.Application.Common.Interfaces.Authentication;
using MusicShop.Application.Services.FullTreeCategories;
using MusicShop.Application.Services.ServiceHandler;
using MusicShop.Infrastructure.Data;
using MusicShop.Application.Services.Authentication;
using MusicShop.Application.Services.JwtTokenGenerator;
using FluentValidation;
using MusicShop.Application.Common.Behavior;
using MusicShop.Presentation.Common.FilterError;
namespace MusicShop.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, ConfigurationManager configuration)
        {
            //app 
            services.AddControllers(options => options.Filters.Add<GlobalErrorHandlingFilter>());
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            //DI
            services.AddScoped<ICategoryServicesHandler, CategoryServicesHandler>();
            services.AddScoped<IFullTreeCategoryService, FullTreeCategoriesService>();
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IAuthService, AuthenticationService>();

            services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<CategoryRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<ProductRequestValidator>();
            //Services
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //Connection string
            services.AddDbContext<DataContext>(options => options.UseSqlServer(configuration.GetConnectionString("DB")));

            return services;
        }
    }
}
