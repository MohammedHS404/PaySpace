using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaySpace.Calculator.Data.Abstractions;
using PaySpace.Calculator.Data.Cache;
using PaySpace.Calculator.Data.Repositories;
using PaySpace.Calculator.Data.Repositories.CalculatorSettings;
using PaySpace.Calculator.Data.Repositories.History;

namespace PaySpace.Calculator.Data.Configuration;

public static class ServiceCollectionExtensions
{
    public static void AddDataServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CalculatorContext>(opt =>
            opt.UseSqlite(configuration.GetConnectionString("CalculatorDatabase")));

        services.AddTransient<ICacheKeyBuilder, CacheKeyBuilder>();

        services.AddScoped<ICacheService, CacheService>();

        services.AddScoped<CalculatorSettingsRepository>();

        services.AddScoped<ICalculatorSettingsRepository, CalculatorSettingsRepositoryCacheProxy>(
            opt => new CalculatorSettingsRepositoryCacheProxy(
                opt.GetRequiredService<CalculatorSettingsRepository>(),
                opt.GetRequiredService<ICacheService>(),
                opt.GetRequiredService<ICacheKeyBuilder>()
            ));

        services.AddScoped<IHistoryRepository, HistoryRepository>();
    }

    public static void InitializeDatabase(this IApplicationBuilder app)
    {
        IServiceScopeFactory scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

        using IServiceScope scope = scopeFactory.CreateScope();

        CalculatorContext context = scope.ServiceProvider.GetRequiredService<CalculatorContext>();

        var pendingMigrations = context.Database.GetPendingMigrations().ToList();

        if (pendingMigrations.Any())
        {
            context.Database.Migrate();
        }
    }
}