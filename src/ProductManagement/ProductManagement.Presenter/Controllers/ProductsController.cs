using Framework.Controller.Extensions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Core.HandlerCommands;

namespace ProductManagement.Presenter.Controllers;


public class ProductsController:BaseControllerV1
{
    private readonly IBus _publishEndpoint;

    public ProductsController(IBus publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync(CreateProductCommandRequest command)
    {
        await _publishEndpoint.Publish(command);
        Task.Run(() => Task.Delay(100));
        return Ok();
    }

}