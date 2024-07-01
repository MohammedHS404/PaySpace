using PaySpace.Calculator.Data.Abstractions;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Data.Repositories.PostalCode;

namespace PaySpace.Calculator.Data.Tests.Repositories;

public class PostalCodeRepositoryTests
{
    [Fact]
    public async Task GetCalculatorTypeByPostalCodeAsync_ShouldReturnCalculatorType_WhenPostalCodeExists()
    {
        // Arrange
        CalculatorContext context = await DatabaseFixture.CreateDatabaseAsync();

        PostalCode postalCode = new()
        {
            Code = "AAAYYY",
            Calculator = CalculatorType.FlatValue
        };
        
        PostalCode postalCode2 = new()
        {
            Code = "8000",
            Calculator = CalculatorType.FlatRate
        };

        await context.PostalCodes.AddAsync(postalCode);
        await context.SaveChangesAsync();

        IPostalCodeRepository repository = new PostalCodeRepository(context);

        // Act
        CalculatorType? calculatorType = await repository.GetCalculatorTypeByPostalCodeAsync("AAAYYY", CancellationToken.None);

        // Assert
        Assert.Equal(CalculatorType.FlatValue, calculatorType);
    }
}