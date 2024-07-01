using PaySpace.Calculator.Web.Services;
using PaySpace.Calculator.Web.Services.Models;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddCalculatorHttpServices(builder.Configuration.GetSection("CalculatorSettings").Get<CalculatorSettingsOptionsDto>());
builder.Services.AddHttpClient();
WebApplication? app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Calculator}/{action=Index}/{id?}");

app.Run();