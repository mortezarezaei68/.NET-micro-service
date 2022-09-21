using Common.Exceptions;
using Framework.Domain.UnitOfWork;
using Framework.Exception.Exceptions.Enum;
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

public class HttpContextScopeFilter:
    IFilter<PublishContext>
    
{
    public async Task Send(PublishContext context, IPipe<PublishContext> next)
    {
        context.TryGetPayload(out IServiceProvider serviceProvider);
        var unitOfWork = (IUnitOfWork)serviceProvider.GetService(typeof(IUnitOfWork));
        try
        {
            if (unitOfWork.HasActiveTransaction) await next.Send(context);
            await using var transaction = await unitOfWork?.BeginTransactionAsync()!;
            await next.Send(context);
            await unitOfWork.CommitAsync(transaction);
        }
        catch (AppException ex)
        {
            unitOfWork.RollbackTransaction();
            throw new AppException(ResultCode.BadRequest, ex.Message);
        }


        await next.Send(context);
    }
    

    public void Probe(ProbeContext context)
    {
    }
}

// public class TransactionConsumerConfigurationObserver : IConsumerConfigurationObserver
// {
//     public void ConsumerConfigured<TConsumer>(IConsumerConfigurator<TConsumer> configurator) where TConsumer : class
//     {
//     }
//
//     public void ConsumerMessageConfigured<TConsumer, TMessage>(
//         IConsumerMessageConfigurator<TConsumer, TMessage> configurator) where TConsumer : class where TMessage : class
//     {
//         configurator.Message(a => a.UseFilter(new HttpContextScopeFilter<TMessage>()));
//     }
// }