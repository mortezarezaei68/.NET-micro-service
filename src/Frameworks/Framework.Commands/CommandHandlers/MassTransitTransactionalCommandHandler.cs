using Common.Exceptions;
using Framework.Domain.UnitOfWork;
using Framework.Exception.Exceptions.Enum;
using MassTransit;
using MassTransit.Transactions;
using Microsoft.Extensions.Logging;

namespace Framework.Commands.CommandHandlers;

public abstract class MassTransitTransactionalCommandHandler<TRequest, TResponse> : IConsumer<TRequest>
    where TRequest : RequestCommand
    where TResponse : ResponseCommand
{
    private readonly IUnitOfWork _unitOfWork;

    protected MassTransitTransactionalCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Consume(ConsumeContext<TRequest> context)
    {
        try
        {
            if (_unitOfWork.HasActiveTransaction) await Handle(context.Message,context.CancellationToken);
            await using var transaction = await _unitOfWork?.BeginTransactionAsync()!;
            var response = await Handle(context.Message,context.CancellationToken);
            await context.RespondAsync(response);
        }
        catch (AppException ex)
        {
            _unitOfWork?.RollbackTransaction();
            throw new AppException(ResultCode.BadRequest, ex.Message);
        }
    }

    protected abstract Task<TResponse> Handle(TRequest command,CancellationToken cancellationToken);
}