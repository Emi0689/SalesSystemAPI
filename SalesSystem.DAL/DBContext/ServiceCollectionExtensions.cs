using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SalesSystem.DAL.DBContext
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSalesDbContext(this IServiceCollection services, IConfiguration config, string environment)
        {
            string conn = string.Empty;

            if (environment == "Development")
            {
                 conn = config.GetConnectionString("stringSQL");
            }
            else
            {
                 conn = config.GetConnectionString("stringSQL");
            }

            return services.AddDbContext<DbsaleContext>(options =>
            {
                options.UseSqlServer(conn);
            });
        }
    }
}
