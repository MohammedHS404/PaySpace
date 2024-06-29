using Microsoft.Extensions.DependencyInjection;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Abstractions.Calculators;
using PaySpace.Calculator.Services.Calculators;
using PaySpace.Calculator.Services.Services;

namespace PaySpace.Calculator.Services.Configuration;

public static class ServiceCollectionExtensions
{
    public static void AddCalculatorServices(this IServiceCollection services)
    {
        services.AddScoped<IPostalCodeService, PostalCodeService>();
        services.AddScoped<IHistoryService, HistoryService>();

        services.AddScoped<IFlatRateCalculator, FlatRateCalculator>();
        services.AddScoped<IFlatValueCalculator, FlatValueCalculator>();
        services.AddScoped<IProgressiveCalculator, ProgressiveCalculator>();
        
        services.AddScoped<ICalculatorFactory, CalculatorFactory>();
        services.AddScoped<ITaxCalculationService, TaxCalculationService>();

        services.AddMemoryCache();
    }
}