using BasketManagement.Core;
using Framework.Controller.Extensions;
using Framework.Exception.DataAccessConfig;
using Framework.Exception.Exceptions;
using Framework.Exception.Exceptions.Enum;
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
        throw new InternalServerException("test", ResultCode.BadRequest);
        return Ok(new TestModel(ResultStatus.Duplicate,"test"));
    }

    public class TestModel : ResponseCommand
    {
        public TestModel(ResultStatus status, string? result) : base(status, result)
        {
        }
    }
}