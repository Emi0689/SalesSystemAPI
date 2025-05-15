using Microsoft.Extensions.DependencyInjection;
using SalesSystem.BLL.Services.Interfaces;
using SalesSystem.BLL.Services;

namespace SalesSystem.BLL
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IRolService, RolService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISaleService, SaleService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
