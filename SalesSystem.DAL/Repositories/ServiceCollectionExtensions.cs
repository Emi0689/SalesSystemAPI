using Microsoft.Extensions.DependencyInjection;
using SalesSystem.DAL.Repositories.Interfaces;

namespace SalesSystem.DAL.Repositories
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ISaleRepository, SaleRepository>();

            return services;
        }
    }
}
