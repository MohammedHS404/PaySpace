using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace PaySpace.Calculator.Data.Tests;

public class DatabaseFixture
{
    private const string InMemoryConnectionString = "DataSource=:memory:";

    public static async Task<CalculatorContext> CreateDatabaseAsync()
    {
        SqliteConnection connection = new(InMemoryConnectionString);

        await connection.OpenAsync();

        DbContextOptions<CalculatorContext> options = new DbContextOptionsBuilder<CalculatorContext>()
            .UseSqlite(connection)
            .Options;

        CalculatorContext context = new(options);

        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        return context;
    }
}