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
        var folder = new Folder("/home/sh0rtener", new ObjectOwnersValue("sh0rtener", "sh0rtener", "any"),
            new ObjectAccessValue(ObjectAccess.ReadWriteExecute, ObjectAccess.ReadWriteExecute,
                ObjectAccess.ReadWriteExecute), null);

        var folder3 = new Folder("test2",
            new ObjectOwnersValue("sh0rtener", "sh0rtener", "any"),
            new ObjectAccessValue(ObjectAccess.ReadWriteExecute, ObjectAccess.ReadWriteExecute,
                ObjectAccess.ReadWriteExecute), folder);

        var file = new FolderFile("/home/sh0rtener/projects/самоанализ_1/выговор.txt", "выговор",
            new ObjectOwnersValue("sh0rtener", "sh0rtener", "any"), new ObjectAccessValue(
                ObjectAccess.ReadWriteExecute, ObjectAccess.ReadWriteExecute,
                ObjectAccess.ReadWriteExecute), folder3, "txt", false);

        Console.WriteLine(file.SizeInMb);
        
        file.Save();

        return Ok(folder);
    }
}