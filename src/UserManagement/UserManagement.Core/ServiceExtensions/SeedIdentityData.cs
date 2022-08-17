using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Core.Domains;
using UserManagement.Core.UserManagementContextConcept;

namespace UserManagement.Core.ServiceExtensions
{
    public static class SeedIdentityData
    {
        public static async Task InitializeUserDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var identityServerDbContext = serviceScope.ServiceProvider.GetRequiredService<UserManagementContext>();
            await identityServerDbContext.Database.MigrateAsync();
        }
    }
}