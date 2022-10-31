using Framework.Controller.Extensions;
using MassTransit;
using MassTransit.Mediator;
using MassTransit.Transactions;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Core;
using OrderManagement.Core.RequestCommand;

namespace OrderManagement.Presenter.Controllers;
public class TestController : BaseController
{
    private readonly IRequestClient<CreateOrderConsumerRequest> _requestClient;
    private readonly IBus _publishEndpoint;

    public TestController(IRequestClient<CreateOrderConsumerRequest> requestClient,
        IBus publishEndpoint)
    {
        _requestClient = requestClient;
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateOrderConsumerRequest request,CancellationToken cancellationToken)
    {
        await _publishEndpoint.Publish(request, cancellationToken);
        //await _mediator.Send(request);
        //var test= await _requestClient.GetResponse<CreateOrderConsumerResponse>(request);

        return Ok();
    }
}