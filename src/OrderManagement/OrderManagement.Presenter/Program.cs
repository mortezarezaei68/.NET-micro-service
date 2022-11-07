using System.Diagnostics;
using System.Reflection;
using System.Transactions;
using Framework.Buses;
using Framework.Commands.CommandHandlers;
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

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("MassTransit", LogEventLevel.Debug)
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<OrderManagementContext>(b =>
{
    b.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        options => { options.CommandTimeout(120); });
    // b.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection"));
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork<OrderManagementContext>>();
builder.Services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
// var bus=Bus.Factory.CreateUsingInMemory(configurator =>
// {
//     configurator.AutoStart = true;
//     configurator.UseTransaction(transactionConfigurator =>
//     {
//         transactionConfigurator.Timeout=TimeSpan.FromSeconds(90);
//         transactionConfigurator.IsolationLevel = IsolationLevel.ReadCommitted;
//     });
//     configurator.ConnectConsumerConfigurationObserver(new UnitOfWorkConsumerConfigurationObserver());
//
// });
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


builder.Services.AddMassTransit(cfg =>
{

    cfg.AddEntityFrameworkOutbox<OrderManagementContext>(o =>
    {
        // configure which database lock provider to use (Postgres, SqlServer, or MySql)
        o.UsePostgres();
        o.IsolationLevel = System.Data.IsolationLevel.ReadCommitted;
        // enable the bus outbox
        o.UseBusOutbox();
    });
    cfg.AddConsumer<CreateOrderConsumer>();
    cfg.AddConsumer<UpdateOrderConsumer>();
    cfg.AddSagaStateMachine<OrderStateMachine, OrderState, RegistrationStateDefinition>()
        // .EntityFrameworkRepository(r =>
        // {
        //     r.ExistingDbContext<OrderManagementContext>();
        //     r.UsePostgres();
        // });
        .EntityFrameworkRepository(r =>
        {
            r.ExistingDbContext<OrderManagementContext>();
            r.UseSqlServer();
        });
    cfg.UsingInMemory((context, cfg) =>
    {
        cfg.AutoStart = true;
        cfg.ConfigureEndpoints(context);
        cfg.ConnectConsumerConfigurationObserver(new UnitOfWorkConsumerConfigurationObserver());
    });
});
builder.Services.AddHostedService<MassTransitConsoleHostedService>();

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

void ConfigureBus(IBusRegistrationContext context, IInMemoryBusFactoryConfigurator configurator)
{
    configurator.ConfigureEndpoints(context);
}