using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Core.UserManagementContextConcept;

namespace UserManagement.Core.ServiceExtensions;

public static class UserManagementContextExtensions
{
    public static void ContextInjection(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<UserManagementContext>(b =>
            b.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                mySqlOptions =>
                {
                    mySqlOptions.CommandTimeout(120);
                }));
    }
}