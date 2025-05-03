using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesSystem.DAL.Repositories.Interfaces;
using SalesSystem.DAL.Repositories;
using SalesSystem.Utility;
using SalesSystem.BLL.Services.Interfaces;
using SalesSystem.BLL.Services;
using SalesSystem.DAL.DBContext;

namespace SalesSystem.IOC
{
    public static class Dependency
    {
        public static void DependencyInjections(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbsaleContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("stringSQL"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISaleRepository>(provider => provider.GetRequiredService<IUnitOfWork>().SaleRepository);
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddScoped<IRolService, RolService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISaleService, SaleService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
