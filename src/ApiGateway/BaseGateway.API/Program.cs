using BaseGateway.API;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;
var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(builder.Environment.WebRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddJsonFile("main-conf.json")
    .AddEnvironmentVariables();

builder.Host.UseSerilog((_, config) =>
{
    config
        .Enrich.FromLogContext()
        .WriteTo.Debug()
        .WriteTo.Console()
        .WriteTo.File("Logs/myapp.txt");
});

builder.Services.AddSwaggerForOcelot(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOcelot();
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
app.UseMiddleware<RequestResponseLoggingMiddleware>();

app.UseSwaggerForOcelotUI(opt =>
{
    opt.PathToSwaggerGenerator = "/swagger/docs";
}).UseOcelot().Wait();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();