using Common.Exceptions;
using Framework.Domain.UnitOfWork;
using Framework.Exception.Exceptions.Enum;
using MassTransit;
using MassTransit.Transactions;
using Microsoft.Extensions.Logging;

namespace Framework.Commands.CommandHandlers;

public abstract class MassTransitTransactionalCommandHandler<TRequest, TResponse> : IConsumer<TRequest>
    where TRequest : RequestCommandData
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
            var response = default(TResponse);
            if (_unitOfWork.HasActiveTransaction) response = await Handle(context.Message);
            await using var transaction = await _unitOfWork?.BeginTransactionAsync()!;
            response = await Handle(context.Message);
            await context.RespondAsync(response);
        }
        catch (AppException ex)
        {
            _unitOfWork.RollbackTransaction();
            throw new AppException(ResultCode.BadRequest, ex.Message);
        }
    }

    protected abstract Task<TResponse> Handle(TRequest command);
}