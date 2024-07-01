using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PaySpace.Calculator.Web.Services.Abstractions;
using PaySpace.Calculator.Web.Services.Dtos;
using PaySpace.Calculator.Web.Services.Services;

namespace PaySpace.Calculator.Web.Services.Configuration;

public static class ServiceCollectionExtensions
{
    public static void AddCalculatorHttpServices(this IServiceCollection services, CalculatorSettingsOptionsDto? calculatorSettingsOptions)
    {
        if (calculatorSettingsOptions == null)
        {
            throw new ArgumentNullException(nameof(calculatorSettingsOptions));
        }
        
        services.AddScoped<ICalculatorIntegrationService, CalculatorIntegrationService>(
            sp=>
            {
                HttpClient httpClient = sp.GetRequiredService<HttpClient>();
                ILogger<CalculatorIntegrationService> logger = sp.GetRequiredService<ILogger<CalculatorIntegrationService>>();
                return new CalculatorIntegrationService(httpClient, calculatorSettingsOptions, logger);
            });
    }
}