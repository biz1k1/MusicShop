﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MusicShop.Application.Common.Interfaces.Authentication;
using MusicShop.Application.Services.Authentication.Identity;
using System.Security.Cryptography;
using System.Text;

namespace MusicShop.Application.Services.JwtTokenGenerator
{
    public static class AddAuthenticationJWT
    {
        public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(JwtSettings), jwtSettings);
            services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings.Issuer,

                        ValidateAudience = true,
                        ValidAudience = jwtSettings.Audience,
                        ValidateLifetime = true,

                        // установка ключа безопасности
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtSettings.Key)),
                        // валидация ключа безопасности
                        ValidateIssuerSigningKey = true,

                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["Not-a-very-tasty-cookie"];
                            return Task.CompletedTask;
                        }
                    };
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy(IdentityData.Admin, x =>
                x.RequireClaim(IdentityData.Admin,"true"));
            });
            return services;
        }
    }
}
