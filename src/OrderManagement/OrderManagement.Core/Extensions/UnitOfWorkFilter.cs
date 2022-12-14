using Common.Exceptions;
using Framework.Domain.UnitOfWork;
using Framework.Exception.Exceptions.Enum;
using MassTransit;

namespace OrderManagement.Core.Extensions;

public class UnitOfWorkFilter<TContext, TMessage> : IFilter<TContext>
    where TContext : class, ConsumeContext<TMessage>
    where TMessage : class
{
    public void Probe(ProbeContext context)
    {
    }

    public async Task Send(TContext context, IPipe<TContext> next)
    {
        Console.WriteLine("Before uow execution....");

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

        Console.WriteLine("After uow execution....");
    }
}