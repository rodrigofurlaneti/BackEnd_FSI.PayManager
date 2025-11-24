using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FSI.PayManager.Domain.Interfaces;
using FSI.PayManager.Infrastructure.Persistence;

namespace FSI.PayManager.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PayManagerDb");

            services.AddDbContext<PayManagerDbContext>(options =>
            {
                options.UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString));
            });

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            return services;
        }
    }
}
