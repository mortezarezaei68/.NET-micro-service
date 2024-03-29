using Framework.UnitOfWork.UnitOfWork;
using MassTransit;

namespace OrderManagement.Core.Extensions;

public class HttpContextScopeFilter:
    IFilter<PublishContext>,
    IFilter<ConsumeContext>,
    IFilter<SendContext>

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
        catch (Exception ex)
        {
            unitOfWork.RollbackTransaction();
            throw new Exception( ex.Message);
        }


        await next.Send(context);
    }


    public async Task Send(ConsumeContext context, IPipe<ConsumeContext> next)
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
        catch (Exception ex)
        {
            unitOfWork.RollbackTransaction();
            throw new Exception(ex.Message);
        }


        await next.Send(context);
    }

    public async Task Send(SendContext context, IPipe<SendContext> next)
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
        catch (Exception ex)
        {
            unitOfWork.RollbackTransaction();
            throw new Exception( ex.Message);
        }


        await next.Send(context);
    }

    public void Probe(ProbeContext context)
    {
    }
}