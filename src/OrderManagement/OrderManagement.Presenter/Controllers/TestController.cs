using MassTransit;
using MassTransit.Mediator;
using MassTransit.Transactions;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Core;
using OrderManagement.Core.RequestCommand;

namespace OrderManagement.Presenter.Controllers;

[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly IRequestClient<CreateOrderConsumerRequest> _requestClient;
    private readonly IBus _publishEndpoint;

    public TestController(IRequestClient<CreateOrderConsumerRequest> requestClient,
        IBus publishEndpoint)
    {
        _requestClient = requestClient;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync(CreateOrderConsumerRequest request,CancellationToken cancellationToken)
    {
        await _publishEndpoint.Publish(request, cancellationToken);
        //await _mediator.Send(request);
        //var test= await _requestClient.GetResponse<CreateOrderConsumerResponse>(request);

        return Ok();
    }
}