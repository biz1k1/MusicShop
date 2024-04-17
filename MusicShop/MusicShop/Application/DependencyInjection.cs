using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MusicShop.Application.Common.Behavior;
using MusicShop.Application.Common.Interfaces.Authentication;
using MusicShop.Application.Services.Authentication;
using MusicShop.Application.Services.Authorization.PermissionService;
using MusicShop.Application.Services.FullTreeCategories;
using MusicShop.Application.Services.JwtTokenGenerator;
using MusicShop.Application.Services.ServiceHandler;
using MusicShop.Application.Services.ServiceHandler.PermissionHandler;
using MusicShop.Application.Services.ServiceHandler.User;
using MusicShop.Domain.Model.Aunth;
using MusicShop.Infrastructure.Data;
using MusicShop.Infrastructure.Repository;
using MusicShop.Presentation.Common.FilterError;
using MusicShop.Domain.Enums;
using MusicShop.Application.Services.DbInitializer;
using Microsoft.Extensions.Options;

namespace MusicShop.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, ConfigurationManager configuration)
        {
            //app 
            services.Configure<AuthorizeOptions>(configuration.GetSection(nameof(AuthorizeOptions)));
            services.AddControllers(options => options.Filters.Add<GlobalErrorHandlingFilter>());

            services.AddControllersWithViews()
            .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
            .AddNewtonsoftJson(options => options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore);
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            //DI
            services.AddScoped<ICategoryServicesHandler, CategoryServicesHandler>();
            services.AddScoped<IAuthenticationServiceHandler, AuthenticationServiceHandler>();
            services.AddScoped<IFullTreeCategoryService, FullTreeCategoriesService>();
            services.AddScoped<IAuthService, AuthenticationService>();
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IPermissionService, PermitionsService>();
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IUserServiceHandler, UserServiceHandler>();

            //Authorization and authentication
            services.AddAuth(configuration);


            services.AddAuthorization(options =>
            {
                options.AddPolicy("Read", policy =>
                policy.AddRequirements(new PermissionRequierment([Permissions.Read])));
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Create", policy =>
                policy.AddRequirements(new PermissionRequierment([Permissions.Create])));
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Update", policy =>
                policy.AddRequirements(new PermissionRequierment([Permissions.Update])));
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Delete", policy =>
                policy.AddRequirements(new PermissionRequierment([Permissions.Delete])));
            });
            

            //Validation
            services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<CategoryRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<ProductRequestValidator>();


            //Services
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //Connection string
            services.AddDbContext<DataContext>(options => options.UseSqlServer(configuration.GetConnectionString("DB")));

            //Repository
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<DbContext, DataContext>();

            return services;
        }





    }
}
