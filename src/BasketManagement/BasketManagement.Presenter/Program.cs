using BasketManagement.Core;
using Framework.Commands.MassTransitDefaultConfig;
using Framework.Common;
using Humanizer;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using SharedLibrary.Core.Messages;

var builder = WebApplication.CreateBuilder(args);

Logs.ConfigureLogging();
builder.Host.UseSerilog();
builder.Services.AddDbContext<BasketManagementContext>(b =>
{
    b.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        options => { options.CommandTimeout(120); });
});


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddCustomSwagger();
builder.Services.AddHttpContextAccessor();
var kafkaConfiguration = builder.Configuration.GetSection("KafkaConfig").Get<List<KafkaConfiguration>>();

builder.Services.MassTransitExtensions<BasketManagementContext>(builder.Configuration, "BasketManagement.Core",
    kafkaConfiguration);
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseCustomSwagger();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

