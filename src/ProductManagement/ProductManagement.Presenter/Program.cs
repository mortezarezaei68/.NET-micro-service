using Cassandra;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using ProductManagement.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
builder.Services.AddDbContext<ProductDbContext>(b =>
{
    b.UseCassandra(builder.Configuration.GetConnectionString("DefaultConnection"),"cv",opt =>
    {
        opt.MigrationsHistoryTable(HistoryRepository.DefaultTableName);
    },o => {

        o.WithQueryOptions(new QueryOptions().SetConsistencyLevel(ConsistencyLevel.LocalOne))
            .WithReconnectionPolicy(new ConstantReconnectionPolicy(1000))
            .WithRetryPolicy(new DefaultRetryPolicy())
            .WithLoadBalancingPolicy(new TokenAwarePolicy(Policies.DefaultPolicies.LoadBalancingPolicy))
            .WithDefaultKeyspace("cv")
            .WithPoolingOptions(
                PoolingOptions.Create()
                    .SetMaxSimultaneousRequestsPerConnectionTreshold(HostDistance.Remote, 1_000_000)
                    .SetMaxSimultaneousRequestsPerConnectionTreshold(HostDistance.Local, 1_000_000)
                    .SetMaxConnectionsPerHost(HostDistance.Local, 1_000_000)
                    .SetMaxConnectionsPerHost(HostDistance.Remote, 1_000_000)
                    .SetMaxRequestsPerConnection(1_000_000)
            );
    } );
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();