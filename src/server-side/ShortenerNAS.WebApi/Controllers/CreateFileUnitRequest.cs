namespace ShortenerNAS.WebApi.Controllers;

public class CreateFileUnitRequest
{
    public required string RootPath { get; set; }
    public required string Name { get; set; }
    public string? Extension { get; set; }
    public string? OriginalPath { get; set; }
}