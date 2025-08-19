using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using application.Models.ImageManagment;
using application.Persistence;
using infrastructure.Repositories.Product;

namespace infrastructure
{
    public static class InfrastructureServiceRegistrationProduct
    {
        public static IServiceCollection AddInfrastructureServicesProduct(this IServiceCollection services,
                                                                    IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWorkProduct>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBaseProduct<>));
            
            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));

            return services;
        }
    }
}
