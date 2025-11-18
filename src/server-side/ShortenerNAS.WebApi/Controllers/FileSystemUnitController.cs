using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShortenerNAS.WebApi.Folders;

namespace ShortenerNAS.WebApi.Controllers;

[ApiController]
[Route("api/fsu")]
public class FileSystemUnitController : ControllerBase
{
    private string _rootFolder;
    private FileSystemUnitService _fileSystemUnitService = new FileSystemUnitService();

    public FileSystemUnitController(IConfiguration configuration)
    {
        _rootFolder = configuration["Constants:RootFolder"] ??
                      throw new InvalidOperationException(
                          "Не задано значение параметра в конфигурации. (Constants:RootFolder)");
    }

    [HttpGet("structure")]
    public IActionResult GetStructure()
    {
        var folder = _fileSystemUnitService.GetFolderByPath(_rootFolder);
        return Ok(folder);
    }

    [HttpPost]
    public IActionResult CreateUnit([FromBody] CreateFileUnitRequest fileSystemUnit)
    {
        var rootFolder = new Folder(fileSystemUnit.RootPath, new("-", "-", "-"),
            new(ObjectAccess.None, ObjectAccess.None, ObjectAccess.None), null);

        if (fileSystemUnit.OriginalPath != null)
        {
            var file = new FolderFile(fileSystemUnit.OriginalPath, fileSystemUnit.Name, new("-", "-", "-"),
                new(ObjectAccess.None, ObjectAccess.None, ObjectAccess.None), rootFolder,
                fileSystemUnit.Extension ?? "");

            rootFolder.AddFile(file);
        }
        else
        {
            var folder = new Folder(fileSystemUnit.Name, new("-", "-", "-"),
                new(ObjectAccess.None, ObjectAccess.None, ObjectAccess.None), rootFolder);
            rootFolder.AddFolder(folder);
        }

        rootFolder.Save();

        var fullFolderStructure = _fileSystemUnitService.GetFolderByPath(_rootFolder);

        return Accepted(fullFolderStructure);
    }

    [HttpDelete("{parentFolder}/{unitName}")]
    public IActionResult DeleteUnit(string parentFolder, string unitName, string? extension, bool isFile)
    {
        parentFolder = parentFolder.Replace("%2F", "/");
        var parts = parentFolder.Split('/');
        var folder = _fileSystemUnitService.GetFolderByPath(_rootFolder);

        var findedFolder = folder.FindFolder(parts);

        var hasUnit = findedFolder.Children.Any(x => x.Name == unitName) ||
                      findedFolder.Files.Any(x => x.Name == unitName);

        if (!hasUnit)
            throw new InvalidDataException($"Папка {parentFolder} не содержит в себе {unitName}");

        if (isFile)
        {
            var file = findedFolder.Files.First(x => x.Name == unitName && x.Extension == extension);
            findedFolder.RemoveFile(file);
        }
        else
        {
            var folderToRemove = findedFolder.Children.First(x => x.Name == unitName);
            folderToRemove.Remove();
        }

        return Accepted(folder);
    }
}