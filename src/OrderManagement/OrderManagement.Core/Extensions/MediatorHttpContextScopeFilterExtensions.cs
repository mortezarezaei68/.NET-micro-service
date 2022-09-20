using Common.Exceptions;
using Framework.Domain.UnitOfWork;
using Framework.Exception.Exceptions.Enum;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace OrderManagement.Core.Extensions;

public static class MediatorHttpContextScopeFilterExtensions
{
    public static void UseHttpContextScopeFilter(this IMediatorConfigurator configurator)
    {
        configurator.ConnectConsumerConfigurationObserver(new TransactionConsumerConfigurationObserver());
    }
}

public class HttpContextScopeFilter<TMessage> :
    IFilter<ConsumeContext<TMessage>> where TMessage : class
{


    public async Task Send(ConsumeContext<TMessage> context, IPipe<ConsumeContext<TMessage>> next)
    {
        var serviceProvider = context.GetPayload<IServiceProvider>();
        var unitOfWork = serviceProvider.GetService<IUnitOfWork>();
       
            try
            {
                if (unitOfWork is {HasActiveTransaction: true}) await next.Send(context);
                await using var transaction = await unitOfWork?.BeginTransactionAsync()!;
                await next.Send(context);
                await unitOfWork.CommitAsync(transaction);
            }
            catch (AppException ex)
            {
                unitOfWork.RollbackTransaction();
                throw new AppException(ResultCode.BadRequest, ex.Message);
            }
        

        await next.Send(context);;
    }

    public void Probe(ProbeContext context)
    {
    }
}

public class TransactionConsumerConfigurationObserver : IConsumerConfigurationObserver
{
    public void ConsumerConfigured<TConsumer>(IConsumerConfigurator<TConsumer> configurator) where TConsumer : class
    {
    }

    public void ConsumerMessageConfigured<TConsumer, TMessage>(
        IConsumerMessageConfigurator<TConsumer, TMessage> configurator) where TConsumer : class where TMessage : class
    {
        configurator.Message(a=>a.UseFilter(new HttpContextScopeFilter<TMessage>()));
    }
}