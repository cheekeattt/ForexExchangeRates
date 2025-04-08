using ForexExchangeRates.Interfaces;
using ForexExchangeRates.Services.ExchangeRate;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<IExchangeRateService, ExchangeRateService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");    
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ExchangeRate}/{action=Index}/{id?}");

app.Run();
