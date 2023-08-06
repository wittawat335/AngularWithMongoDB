using Demo.Domain.Models.Base;
using Demo.Domain.Models.Base.Interfaces;
using Demo.Domain.RepositoryContract;
using Demo.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Demo.Infrastructure
{
    public static class InfraConfiguration
    {
        public static void InjectDependence(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDBSetting"));
            services.AddSingleton<IMongoDbSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
        }
    }
}
