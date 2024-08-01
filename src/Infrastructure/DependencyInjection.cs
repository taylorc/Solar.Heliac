using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Solar.Heliac.Application.Common.Interfaces;
using Solar.Heliac.Infrastructure.Persistence;
using Solar.Heliac.Infrastructure.Persistence.Interceptors;

namespace Solar.Heliac.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<EntitySaveChangesInterceptor>();
        services.AddScoped<DispatchDomainEventsInterceptor>();

        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
        {
            options.AddInterceptors(
                services.BuildServiceProvider().GetRequiredService<EntitySaveChangesInterceptor>(),
                services.BuildServiceProvider().GetRequiredService<DispatchDomainEventsInterceptor>()
            );

            options.UseSqlServer(config.GetConnectionString("DefaultConnection"), builder =>
            {
                builder.MigrationsAssembly(typeof(DependencyInjection).Assembly.FullName);
                builder.EnableRetryOnFailure();
            });
        });

        services.AddSingleton(TimeProvider.System);

        return services;
    }
}