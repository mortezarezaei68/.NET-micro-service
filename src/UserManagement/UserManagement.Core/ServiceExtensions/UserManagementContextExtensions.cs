using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Core.Domains;
using UserManagement.Core.UserManagementContextConcept;

namespace UserManagement.Core.ServiceExtensions;

public static class UserManagementContextExtensions
{
    public static void ContextInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UserManagementContext>(b =>
        {
            b.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                options => { options.CommandTimeout(120); });
            b.UseOpenIddict();
        });
        services.AddIdentity<User, Role>().AddEntityFrameworkStores<UserManagementContext>().AddDefaultTokenProviders();;
    }
}