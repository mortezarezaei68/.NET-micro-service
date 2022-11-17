using System.Diagnostics;
using System.Reflection;
using System.Transactions;
using Framework.Buses;
using Framework.Commands.CommandHandlers;
using Framework.Commands.MassTransitDefaultConfig;
using Framework.Common;
using Framework.Domain.UnitOfWork;
using Framework.UnitOfWork;
using MassTransit;
using MassTransit.Mediator;
using MassTransit.Metadata;
using MassTransit.Transactions;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OrderManagement.Core;
using OrderManagement.Core.Extensions;
using OrderManagement.Core.RequestCommand;
using Serilog;
using Serilog.Events;



var builder = WebApplication.CreateBuilder(args);
Logs.ConfigureLogging();

builder.Host.UseSerilog();


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<OrderManagementContext>(b =>
{
    b.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        options => { options.CommandTimeout(120); });
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork<OrderManagementContext>>();
builder.Services.AddOpenTelemetryTracing(x =>
{
    x.SetResourceBuilder(ResourceBuilder.CreateDefault()
            .AddService("api")
            .AddTelemetrySdk()
            .AddEnvironmentVariableDetector())
        .AddSource("MassTransit")
        .AddAspNetCoreInstrumentation()
        .AddJaegerExporter(o =>
        {
            o.AgentHost = HostMetadataCache.IsRunningInContainer ? "jaeger" : "localhost";
            o.AgentPort = 6831;
            o.MaxPayloadSizeInBytes = 4096;
            o.ExportProcessorType = ExportProcessorType.Batch;
            o.BatchExportProcessorOptions = new BatchExportProcessorOptions<Activity>
            {
                MaxQueueSize = 2048,
                ScheduledDelayMilliseconds = 5000,
                ExporterTimeoutMilliseconds = 30000,
                MaxExportBatchSize = 512,
            };
        });
});
var kafkaConfiguration = builder.Configuration.GetSection("KafkaConfig").Get<List<KafkaConfiguration>>();

builder.Services.MassTransitExtensions<OrderManagementContext>(builder.Configuration, "OrderManagement.Core",
    kafkaConfiguration);

builder.Services.AddCustomSwagger();


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