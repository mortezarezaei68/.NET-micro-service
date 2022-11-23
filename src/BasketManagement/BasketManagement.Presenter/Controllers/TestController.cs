using BasketManagement.Core;
using Framework.Controller.Extensions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace BasketManagement.Presenter.Controllers;

[ApiController]    
[ApiResultFilter]
[ApiExplorerSettings(GroupName = "v1")]
[ApiVersion("1.0")]
// [Route("api/[controller]")]
[Route("api/v{version:apiVersion}/[controller]")]
public class TestController: ControllerBase
{
    private readonly ITopicProducer<KafkaMessage> _busProducer;

    public TestController(ITopicProducer<KafkaMessage> busProducer)
    {
        _busProducer = busProducer;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync()
    {
        await _busProducer.Produce(new KafkaMessage()
        {
            Text = "test"
        });
        return Ok();
    }
}