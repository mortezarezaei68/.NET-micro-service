using Framework.Controller.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace OrderManagement.Presenter.Controllers.V2;

[ApiController]    
[ApiResultFilter]
[ApiExplorerSettings(GroupName = "v2")]
[ApiVersion("2.0")]
// [Route("api/[controller]")]
[Route("api/v{version:apiVersion}/[controller]")]
public class BaseControllerV2 : ControllerBase
{
    public bool UserIsAutheticated => HttpContext.User.Identity.IsAuthenticated;
}