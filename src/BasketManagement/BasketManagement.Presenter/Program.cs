using BasketManagement.Core;
using Framework.Commands.MassTransitDefaultConfig;
using Framework.Common;
using Framework.Exception.Exceptions.Extensions;
using Hellang.Middleware.ProblemDetails;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Serilog;
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
builder.Services.AddCustomProblemDetails();

builder.Services.AddCustomSwagger();
builder.Services.AddHttpContextAccessor();


// var kafkaConfiguration = builder.Configuration.GetSection("KafkaConfig").Get<List<KafkaConfiguration>>();

// builder.Services.AddMasstransitConsumerProducerExtension<BasketManagementContext>(builder.Configuration, nameof(BasketManagement.Core));
// builder.Services.AddMassTransit(x =>
// {
//     x.UsingInMemory((context, cfg) => cfg.ConfigureEndpoints(context));
//
//     x.AddRider(rider =>
//     {
//         rider.AddProducer<KafkaMessage>(nameof(KafkaMessage).Underscore());
//         rider.AddProducer<MarketingQueueMessage>(nameof(MarketingQueueMessage).Underscore());
//
//         rider.UsingKafka((context, k) =>
//         {
//             k.Host(new List<string> {"localhost:9092"});
//         });
//     });
// });
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
app.UseProblemDetails();
app.UseRouting();

app.UseAuthorization();
app.UseCustomSwagger();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

