using Microsoft.AspNetCore.Mvc;
using ShortenerNAS.WebApi.Folders;

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

    [HttpGet("createfolder")]
    public IActionResult CreateFolder()
    {
        var service = new FileSystemUnitService();
        var folder = service.GetFolderByPath("/home/sh0rtener/test");

        return Ok(folder);
    }
}