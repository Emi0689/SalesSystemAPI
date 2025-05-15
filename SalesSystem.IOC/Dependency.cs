using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesSystem.DAL.Repositories;
using SalesSystem.Utility;
using SalesSystem.DAL.DBContext;
using SalesSystem.BLL;

namespace SalesSystem.IOC
{
    public static class Dependency
    {
        public static void DependencyInjections(this IServiceCollection services, IConfiguration configuration, string environment)
        {
            services.AddSalesDbContext(configuration, environment);
            services.AddRepositories();
            services.AddServices();
            services.AddUtilities();
        }
    }
}
