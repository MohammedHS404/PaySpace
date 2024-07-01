using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Data.Repositories.CalculatorSettings;

namespace PaySpace.Calculator.Data.Tests.Repositories;

public class CalculatorSettingsRepositoryTests
{
    [Fact]
    public async Task GetSettingsAsync_ShouldReturnCalculatorSettingsWithCorrectType()
    {
        CalculatorContext context = await DatabaseFixture.CreateDatabaseAsync();

        context.CalculatorSettings.Add(new CalculatorSetting
        {
            Calculator = CalculatorType.FlatValue,
            From = 0,
            To = 1000,
            Rate = 0.1m,
            RateType = RateType.Amount
        });
        
        context.CalculatorSettings.Add(new CalculatorSetting
        {
            Calculator = CalculatorType.FlatValue,
            From = 1001,
            To = 2000,
            Rate = 0.2m,
            RateType = RateType.Amount
        });
        
        await context.SaveChangesAsync();

        CalculatorSettingsRepository repository = new(context);

        List<CalculatorSetting> settings = await repository.GetSettingsAsync(CalculatorType.FlatValue, CancellationToken.None);

        Assert.NotEmpty(settings);

        Assert.All(settings, s => Assert.Equal(CalculatorType.FlatValue, s.Calculator));
    }
}