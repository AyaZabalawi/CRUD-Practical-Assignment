using ServiceContracts;
using Services;
using StocksAppProject;

var builder = WebApplication.CreateBuilder(args);

//Services
builder.Services.AddControllersWithViews();
builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));
builder.Services.AddSingleton<IStockService, StocksService>();
builder.Services.AddSingleton<IFinnhubService, FinnhubService>();
builder.Services.AddHttpClient();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
