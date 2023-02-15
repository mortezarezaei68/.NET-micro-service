using Framework.Controller.Extensions;
using Mapster;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Commands;
using ProductManagement.Presenter.ViewModels;

namespace ProductManagement.Presenter.Controllers;

public class ProductsController : BaseControllerV1
{
    private readonly IBus _publishEndpoint;

    public ProductsController(IBus publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync(CreateProductModel command,
        CancellationToken cancellationToken)
    {
        await _publishEndpoint.Publish(command.Adapt<CreateProductCommandRequest>(), cancellationToken);
        return Ok();
    }

    [HttpPut("{id}/details")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateProductDetailAsync(string id, [FromBody] CreateProductDetailModel request,
        CancellationToken cancellationToken)
    {
        await _publishEndpoint.Publish((id, request).Adapt<CreateProductDetailCommandRequest>(), cancellationToken);
        return NoContent();
    }
}