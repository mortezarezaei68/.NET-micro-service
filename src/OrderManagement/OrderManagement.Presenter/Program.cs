using System.Reflection;
using System.Transactions;
using Framework.Buses;
using Framework.Commands.CommandHandlers;
using Framework.Common;
using Framework.Domain.UnitOfWork;
using Framework.UnitOfWork;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OrderManagement.Core;
using OrderManagement.Core.Extensions;
using OrderManagement.Core.RequestCommand;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<OrderManagementContext>(b =>
{
    b.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        options => { options.CommandTimeout(120); });
});
builder.Services.AddScoped<IUnitOfWork,UnitOfWork<OrderManagementContext>>();
builder.Services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
var bus=Bus.Factory.CreateUsingInMemory(configurator =>
{
    configurator.AutoStart = true;
    configurator.UseTransaction(transactionConfigurator =>
    {
        transactionConfigurator.Timeout=TimeSpan.FromSeconds(90);
        transactionConfigurator.IsolationLevel = IsolationLevel.ReadCommitted;
    });
});
builder.Services.AddMassTransit(cfg =>
{
    cfg.UsingInMemory((context, cfg) =>
    {
        cfg.UseMessageRetry(retryConfiguration =>
        {
            retryConfiguration.Intervals(2000, 4000, 10000); // in ms
        });
         cfg.ConfigureEndpoints(context);
        //cfg.ConnectConsumerConfigurationObserver(new UnitOfWorkConsumerConfigurationObserver());
    });
   
    cfg.AddTransactionalBus();
    cfg.AddMediator(configurator =>
    {
        configurator.AddConsumer<CreateOrderConsumer>();
        configurator.AddConsumer<UpdateOrderConsumer>();
        configurator.ConfigureMediator((context, cfg) => cfg.UseHttpContextScopeFilter(context));
        configurator.AddSagaStateMachine<OrderStateMachine, OrderState>()
            .InMemoryRepository();
    });

});
//builder.Services.AddMassTransitHostedService(new MassTransitConsoleHostedService(bus));
// builder.Services.AddMediator(configurator =>
// {
//     var test=configurator.GetType().BaseType.Name.Contains(
//         nameof(MassTransitTransactionalCommandHandler<RequestCommandData, ResponseCommand>));
//     configurator.AddConsumer<CreateOrderConsumer>();
//     // configurator.AddConsumers(type =>
//     // {
//     //     var data=type.BaseType?.Name.Contains(
//     //         nameof(MassTransitTransactionalCommandHandler<RequestCommandData, ResponseCommand>)) ?? false;
//     //     return data;
//     // }, typeof(RequestCommandData).Assembly);
//     configurator.AddRequestClient<CreateOrderConsumerRequest>();
//     configurator.ConfigureMediator((context, cfg) => cfg.UseHttpContextScopeFilter(context));
// });

builder.Services.AddHostedService<MassTransitConsoleHostedService>();
//
// builder.Services.AddMassTransit(cfg =>
// {
//     cfg.SetKebabCaseEndpointNameFormatter();
//
//  
//     cfg.UsingInMemory(ConfigureBus);
//
// });

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