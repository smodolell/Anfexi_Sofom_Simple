using Anfx.Profuturo.Sofom.Application.Common.Interfaces;
using Anfx.Profuturo.Sofom.Infrastructure.Persitence;
using Anfx.Profuturo.Sofom.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Anfx.Profuturo.Sofom.Infrastructure;

public static class PersitenceServiceRegistration
{

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "Connection string 'DefaultConnection' not found in configuration");

        // Register DbContext with SQL Server
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(
                connectionString,
                sqlOptions =>
                {
                    sqlOptions.CommandTimeout(30);
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 3);
                }
            );

            // Enable detailed logging in development
            var environment = configuration["ASPNETCORE_ENVIRONMENT"];
            if (environment?.Equals("Development", StringComparison.OrdinalIgnoreCase) ?? false)
            {
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
                options.LogTo(Console.WriteLine, LogLevel.Information);
            }
        });
        services.AddDbContextFactory<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        }, ServiceLifetime.Transient);

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());


        // Register Unit of Work
        services.AddScoped<IDatabaseService, DatabaseService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IConsecutivoService, ConsecutivoService>();

        // Register repositories 
        //Solicitud





        return services;
    }
}
