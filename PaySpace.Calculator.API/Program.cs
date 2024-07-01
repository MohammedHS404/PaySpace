
using PaySpace.Calculator.Data;
using PaySpace.Calculator.Data.Configuration;
using PaySpace.Calculator.Business.Configuration;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddCalculatorServices();
builder.Services.AddDataServices(builder.Configuration);

WebApplication? app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();
app.InitializeDatabase();

app.Run();