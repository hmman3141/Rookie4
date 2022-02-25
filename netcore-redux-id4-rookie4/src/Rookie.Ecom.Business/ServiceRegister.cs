using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rookie.Ecom.Business.Interfaces;
using Rookie.Ecom.Business.Services;
using Rookie.Ecom.DataAccessor;
using System.Reflection;
using Refit;
using System;

namespace Rookie.Ecom.Business
{
    public static class ServiceRegister
    {
        public static void AddBusinessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataAccessorLayer(configuration);
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductPictureService, ProductPictureService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRoleService, UserRoleService>();
            services.AddTransient<IUserAddressService, UserAddressService>();
            services.AddTransient<IRatingService, RatingService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IOrderDetailService, OrderDetailService>();
            services.AddRefitClient<IIdentityProviderService>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:5001"));
        }
    }
}