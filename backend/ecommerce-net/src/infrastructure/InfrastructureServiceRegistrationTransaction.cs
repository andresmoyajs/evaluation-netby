using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using application.Models.ImageManagment;
using application.Persistence;
using infrastructure.Repositories.Product;
using infrastructure.Repositories.Transaction;

namespace infrastructure
{
    public static class InfrastructureServiceRegistrationTransaction
    {
        public static IServiceCollection AddInfrastructureServicesTransaction(this IServiceCollection services,
                                                                    IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWorkTransaction>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBaseTransaction<>));
            
            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));

            return services;
        }
    }
}
