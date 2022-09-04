using Microsoft.AspNetCore.Mvc;

namespace Mobile.API.Controllers;

[Route("api/[controller]")]
public class TestMobileController:ControllerBase
{
    [HttpGet("v1/[action]")]
    public IActionResult GetAsync()
    {
        return Ok("test");
    }
}