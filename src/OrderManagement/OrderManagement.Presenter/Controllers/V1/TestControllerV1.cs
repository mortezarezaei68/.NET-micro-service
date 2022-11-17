using Framework.Controller.Extensions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Core.RequestCommand;

namespace OrderManagement.Presenter.Controllers.V1;

[ApiController]    
[ApiResultFilter]
[ApiExplorerSettings(GroupName = "v1")]
[ApiVersion("1.0")]
// [Route("api/[controller]")]
[Route("api/v{version:apiVersion}/[controller]")]
public class TestControllerV1 : ControllerBase
{
    private readonly IRequestClient<CreateOrderConsumerRequest> _requestClient;
    private readonly IBus _publishEndpoint;

    public TestControllerV1(IRequestClient<CreateOrderConsumerRequest> requestClient,
        IBus publishEndpoint)
    {
        _requestClient = requestClient;
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost]
    public  async Task<IActionResult> CreateAsync(CreateOrderConsumerRequest request,CancellationToken cancellationToken)
    {
        await _publishEndpoint.Publish(request, cancellationToken);
        //await _mediator.Send(request);
        //var test= await _requestClient.GetResponse<CreateOrderConsumerResponse>(request);

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        return Ok();
    }
    
}