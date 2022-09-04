using Framework.Buses;
using Microsoft.AspNetCore.Mvc;

namespace UserManagement.Presenter.Controllers;

[Route("api/[controller]")]
public class TestController:ControllerBase
{
    private readonly ICapEventBus _capEventBus;

    public TestController(ICapEventBus capEventBus)
    {
        _capEventBus = capEventBus;
    }

    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken=default)
    {
        await _capEventBus.Publish(DateTime.Now, "test", cancellationToken);
        return Ok("test");
    }
}