using Framework.Controller.Extensions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Core.HandlerCommands;

namespace ProductManagement.Presenter.Controllers.V1;

[ApiExplorerSettings(GroupName = "v1")]
[ApiVersion("1.0")]
public class ProductController:BaseControllerV1
{
    private readonly IPublishEndpoint _publishEndpoint;

    public ProductController(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync(CreateProductCommandRequest command)
    {
        await _publishEndpoint.Publish(command);
        return Ok();
    }
}