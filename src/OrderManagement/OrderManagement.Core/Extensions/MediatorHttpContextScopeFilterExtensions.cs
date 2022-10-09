using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace OrderManagement.Core.Extensions;

public static class MediatorHttpContextScopeFilterExtensions
{
    public static void UseHttpContextScopeFilter(this IMediatorConfigurator configurator,IServiceProvider serviceProvider)
    {
        var filter = new HttpContextScopeFilter();

    }
}