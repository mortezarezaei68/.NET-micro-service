using Framework.Controller.Extensions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Core.RequestCommand;
using OrderManagement.Presenter.Controllers.V1;

namespace OrderManagement.Presenter.Controllers.V2;

// [ApiController]    
// [ApiResultFilter]
// [ApiExplorerSettings(GroupName = "v2")]
// [ApiVersion("2.0")]
// // [Route("api/[controller]")]
// [Route("api/v{version:apiVersion}/[controller]")]
public class TestTwoController:BaseControllerV2
{
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        return Ok();
    }
}