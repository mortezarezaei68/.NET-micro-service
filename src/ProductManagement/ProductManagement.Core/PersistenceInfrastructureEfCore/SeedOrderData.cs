using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ProductManagement.Core.PersistenceInfrastructureEfCore
{
    public static class SeedOrderData
    {
        public static async Task InitializeProductDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
            var serverDbContext = serviceScope?.ServiceProvider.GetRequiredService<ProductDbContext>();
            await serverDbContext?.Database.MigrateAsync()!;
            

        }
    }
}