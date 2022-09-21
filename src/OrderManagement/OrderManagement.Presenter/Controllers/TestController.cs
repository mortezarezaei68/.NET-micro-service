using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Core;
using OrderManagement.Core.RequestCommand;

namespace OrderManagement.Presenter.Controllers;

[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly IRequestClient<CreateOrderConsumerRequest> _requestClient;
    private readonly IMediator _mediator;
    private readonly IPublishEndpoint _publishEndpoint;

    public TestController(IRequestClient<CreateOrderConsumerRequest> requestClient, IMediator mediator,
        IPublishEndpoint publishEndpoint)
    {
        _requestClient = requestClient;
        _mediator = mediator;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync(CreateOrderConsumerRequest request)
    {
        await _mediator.Publish(request);
        //await _mediator.Send(request);
        //var test= await _requestClient.GetResponse<CreateOrderConsumerResponse>(request);

        return Ok();
    }
}