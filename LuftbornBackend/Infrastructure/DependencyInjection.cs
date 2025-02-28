using Application.Interfaces.Repository;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Persistence.Repository;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPostRepository, PostRepository>();

            return services;

        }
    }   
}
