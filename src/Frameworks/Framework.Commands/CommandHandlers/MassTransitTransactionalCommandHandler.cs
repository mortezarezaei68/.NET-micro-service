using Common.Exceptions;
using Framework.Domain.UnitOfWork;
using Framework.Exception.Exceptions.Enum;
using MassTransit;
using MassTransit.Transactions;
using Microsoft.Extensions.Logging;

namespace Framework.Commands.CommandHandlers;

public class MassTransitTransactionalCommandHandler<TRequest,TResponse>: IConsumer<TRequest>
    where TRequest : RequestCommandData
    where TResponse : ResponseCommand
{
    private readonly ITransactionalBus _transactionalBus;

    public MassTransitTransactionalCommandHandler(ITransactionalBus transactionalBus)
    {
        _transactionalBus = transactionalBus;
    }

    public async Task Consume(ConsumeContext<TRequest> context)
    {
        var result = await Handle(context.Message);
        await _transactionalBus.Send(context);
        await context.RespondAsync(result);
    }

    public virtual async Task<TResponse> Handle(TRequest command)
    {
        try
        {

            var response = await Handle(command);
            await _transactionalBus.Release(); 
            return response;
        }
        catch (AppException ex)
        {
            throw new AppException(ResultCode.BadRequest,ex.Message);
        }
    }
}