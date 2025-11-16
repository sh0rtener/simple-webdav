using Microsoft.AspNetCore.Mvc;

namespace ShortenerNAS.WebApi.Controllers;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult GetIndex()
    {
        return Ok("this is a test");
    }
}