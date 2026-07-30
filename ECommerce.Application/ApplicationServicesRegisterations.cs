using AutoMapper;
using ECommerce.Application.Contracts;
using ECommerce.Application.Profiles;
using ECommerce.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application
{
    public static class ApplicationServicesRegisterations
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(c => c.AddProfile(new ProductProfile()), typeof(ApplicationServicesRegisterations).Assembly);
            services.AddScoped<IProductServices, ProductServices>();
            services.AddScoped<IBasketServices, BasketServices>();
            services.AddSingleton<ICacheService, CacheServices>();
            services.AddScoped<IIdentityServices, IdentityServices>();
            services.AddScoped<IAuthenticationServices, AuthenticationServices>();
            services.AddScoped<ITokenServices, TokenServices>();

            return services;

        }

    }
}
