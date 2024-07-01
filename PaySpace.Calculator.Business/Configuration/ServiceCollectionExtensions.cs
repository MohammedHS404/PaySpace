using Microsoft.Extensions.DependencyInjection;
using PaySpace.Calculator.Business.Abstractions;
using PaySpace.Calculator.Business.Abstractions.Calculators;
using PaySpace.Calculator.Business.Calculators;
using PaySpace.Calculator.Business.Services;

namespace PaySpace.Calculator.Business.Configuration;

public static class ServiceCollectionExtensions
{
    public static void AddCalculatorServices(this IServiceCollection services)
    {
        services.AddScoped<IHistoryService, HistoryService>();

        services.AddScoped<IFlatRateCalculator, FlatRateCalculator>();
        services.AddScoped<IFlatValueCalculator, FlatValueCalculator>();
        services.AddScoped<IProgressiveCalculator, ProgressiveCalculator>();
        
        services.AddScoped<ICalculatorFactory, CalculatorFactory>();
        services.AddScoped<ITaxCalculationService, TaxCalculationService>();
        
        services.AddScoped<IPostalCodeService, PostalCodeService>();

        services.AddMemoryCache();
    }
}