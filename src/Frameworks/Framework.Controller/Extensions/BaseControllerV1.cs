using Microsoft.AspNetCore.Mvc;

namespace Framework.Controller.Extensions
{
    [ApiController]    
    [ApiResultFilter]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseControllerV1 : ControllerBase
    {
        public bool UserIsAutheticated => HttpContext.User.Identity.IsAuthenticated;
    }
}