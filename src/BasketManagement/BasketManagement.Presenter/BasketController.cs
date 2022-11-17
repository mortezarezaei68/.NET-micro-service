using Framework.Controller.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BasketManagement.Presenter;

[ApiController]    
[ApiResultFilter]
[ApiExplorerSettings(GroupName = "v1")]
[ApiVersion("1.0")]
// [Route("api/[controller]")]
[Route("api/v{version:apiVersion}/[controller]")]
public class BasketController:ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        return Ok();
    }
}