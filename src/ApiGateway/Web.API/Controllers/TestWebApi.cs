using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers;

[Route("api/[controller]")]
public class TestWebApi:ControllerBase
{
    [HttpGet("v1/[action]")]
    public IActionResult GetAsync()
    {
        return Ok("test");
    }
}