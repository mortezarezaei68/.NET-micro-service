using Framework.Commands.MassTransitDefaultConfig;
using Framework.Common;
using Framework.Exception.Exceptions.Extensions;
using Framework.UnitOfWork;
using Framework.UnitOfWork.UnitOfWork;
using Hellang.Middleware.ProblemDetails;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Configurations.Ef;
using ProductManagement.Domains.Repositories;
using ProductManagement.Ef;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomProblemDetails();
builder.Services.AddCustomSwagger();
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ProductDbContext>(b =>
{    b.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    options => { options.CommandTimeout(120); });
});

builder.Services.AddTransient(typeof(UnitOfWork<>));
builder.Services.AddScoped<IUnitOfWork,UnitOfWork<ProductDbContext>>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddMasstransitConsumerProducerExtension<ProductDbContext>(builder.Configuration, nameof(ProductManagement.Command.Handlers));
builder.Services.AddCustomMapster();

Logs.ConfigureLogging();
builder.Host.UseSerilog();
var app = builder.Build();

app.UseProblemDetails();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.InitializeProductDatabase().GetAwaiter().GetResult();
app.UseRouting();

app.UseAuthorization();
app.UseCustomSwagger();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();