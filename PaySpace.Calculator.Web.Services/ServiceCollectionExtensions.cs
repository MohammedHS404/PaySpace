using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PaySpace.Calculator.Web.Services.Abstractions;
using PaySpace.Calculator.Web.Services.Models;

namespace PaySpace.Calculator.Web.Services;

public static class ServiceCollectionExtensions
{
    public static void AddCalculatorHttpServices(this IServiceCollection services, CalculatorSettingsOptionsDto? calculatorSettingsOptions)
    {
        if (calculatorSettingsOptions == null)
        {
            throw new ArgumentNullException(nameof(calculatorSettingsOptions));
        }
        
        services.AddScoped<ICalculatorHttpService, CalculatorHttpService>(
            sp=>
            {
                HttpClient httpClient = sp.GetRequiredService<HttpClient>();
                ILogger<CalculatorHttpService> logger = sp.GetRequiredService<ILogger<CalculatorHttpService>>();
                return new CalculatorHttpService(httpClient, calculatorSettingsOptions, logger);
            });
    }
}