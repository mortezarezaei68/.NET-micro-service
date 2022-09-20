using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Abstractions;
using UserManagement.Core.Domains;
using UserManagement.Core.UserManagementContextConcept;

namespace UserManagement.Core.ServiceExtensions
{
    public static class SeedIdentityData
    {
        public static async Task InitializeUserDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
            var identityServerDbContext = serviceScope?.ServiceProvider.GetRequiredService<UserManagementContext>();
            var manager = serviceScope?.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();
            await identityServerDbContext?.Database.MigrateAsync()!;
   

            if (await manager.FindByClientIdAsync("balosar-blazor-client") is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "balosar-blazor-client",
                    ConsentType = OpenIddictConstants.ConsentTypes.Explicit,
                    DisplayName = "Blazor client application",
                    Type = OpenIddictConstants.ClientTypes.Public,
                    PostLogoutRedirectUris =
                    {
                        new Uri("http://localhost:3000/signout-callback.html")
                    },
                    RedirectUris =
                    {
                        new Uri("http://localhost:3000/signin-callback.html")
                    },
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Authorization,
                        OpenIddictConstants.Permissions.Endpoints.Logout,
                        OpenIddictConstants.Permissions.Endpoints.Token,
                        OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                        OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                        OpenIddictConstants.Permissions.ResponseTypes.Code,
                        OpenIddictConstants.Permissions.Scopes.Email,
                        OpenIddictConstants.Permissions.Scopes.Profile,
                        OpenIddictConstants.Permissions.Scopes.Roles
                    }
                });
            }
            if (await manager.FindByClientIdAsync("console") == null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "console",
                    ClientSecret = "388D45FA-B36B-4988-BA59-B187D329C207",
                    DisplayName = "My client application",
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Token,
                        OpenIddictConstants.Permissions.GrantTypes.ClientCredentials
                    }
                });
            }
        }
    }
}