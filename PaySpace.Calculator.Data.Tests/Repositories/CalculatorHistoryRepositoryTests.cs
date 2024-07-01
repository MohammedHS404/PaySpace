using Microsoft.EntityFrameworkCore.Migrations;
using PaySpace.Calculator.Data.Dtos;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Data.Repositories.History;

namespace PaySpace.Calculator.Data.Tests.Repositories;

public class CalculatorHistoryRepositoryTests
{
    [Theory]
    [InlineData(1, 1, 1)]
    [InlineData(1, 2, 2)]
    [InlineData(2, 1, 1)]
    public async Task GetHistoryAsync_ShouldReturnHistoriesBasedOnPagination(int page, int pageSize, int expectedCount)
    {
        CalculatorContext context = await DatabaseFixture.CreateDatabaseAsync();

        context.CalculatorHistories.Add(new CalculatorHistory()
        {
            Calculator = CalculatorType.FlatValue,
            PostalCode = "7441",
            Tax = 200,
            Income = 6000,
            Timestamp = DateTime.Now
        });

        context.CalculatorHistories.Add(new CalculatorHistory
        {
            Calculator = CalculatorType.Progressive,
            PostalCode = "A100",
            Tax = 200,
            Income = 6000,
            Timestamp = DateTime.Now
        });

        context.CalculatorHistories.Add(new CalculatorHistory
        {
            Calculator = CalculatorType.FlatValue,
            PostalCode = "A100",
            Tax = 200,
            Income = 6000,
            Timestamp = DateTime.Now
        });

        await context.SaveChangesAsync();

        CalculatorHistoryRepository repository = new(context);

        List<CalculatorHistory> histories = await repository.GetHistoriesAsync(new PaginationDto
        {
            PageNumber = page,
            PageSize = pageSize
        }, CancellationToken.None);

        Assert.NotEmpty(histories);
        Assert.Equal(pageSize, histories.Count);
        Assert.Equal(expectedCount, histories.Count);
    }
}