using Microsoft.EntityFrameworkCore;
using MusicShop.Application.Common.Interfaces.Authentication;
using MusicShop.Application.Services.FullTreeCategories;
using MusicShop.Infrastructure.Data;
using MusicShop.Application.Services.Authentication;
using MusicShop.Application.Services.JwtTokenGenerator;
using FluentValidation;
using MusicShop.Application.Common.Behavior;
using MusicShop.Presentation.Common.FilterError;
using MusicShop.Infrastructure.Repository;
using MusicShop.Domain.Model;
using MusicShop.Application.Services.ServiceHandler;


namespace MusicShop.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, ConfigurationManager configuration) {
            //app 
            services.AddControllers(options => options.Filters.Add<GlobalErrorHandlingFilter>());
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            //DI
            services.AddScoped<ICategoryServicesHandler, CategoryServicesHandler>();
            services.AddScoped<IAuthenticationServiceHandler, AuthenticationServiceHandler>();
            services.AddScoped<IFullTreeCategoryService, FullTreeCategoriesService>();
            services.AddScoped<IAuthService, AuthenticationService>();
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddAuth(configuration);

            services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<CategoryRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<ProductRequestValidator>();


            //Services
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //Connection string
            services.AddDbContext<DataContext>(options => options.UseSqlServer(configuration.GetConnectionString("DB")));


            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<DbContext, DataContext>();

            return services;
        }
        
    }
}
