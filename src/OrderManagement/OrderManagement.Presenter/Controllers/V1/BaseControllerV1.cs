using Framework.Controller.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace OrderManagement.Presenter.Controllers.V1
{
    [ApiController]    
    [ApiResultFilter]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiVersion("1.0")]
    // [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseControllerV1 : ControllerBase
    {
        public bool UserIsAutheticated => HttpContext.User.Identity.IsAuthenticated;
    }
}