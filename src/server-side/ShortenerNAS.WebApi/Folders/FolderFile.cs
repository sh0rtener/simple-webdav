namespace ShortenerNAS.WebApi.Folders;

public class FolderFile : FileSystemUnit
{
    private string _originalPath;
    public Folder ParentDirectory { get; private set; }
    public string Extension { get; private set; }
    public int SizeInMb => GetSizeInBytes() * 1024;
    public bool IsExecutable { get; private set; }

    public FolderFile(string originalPath, string name, ObjectOwnersValue objectOwnersValue,
        ObjectAccessValue objectAccessValue, Folder parent, string extension, bool isExecutable = false) : base(name,
        objectOwnersValue, objectAccessValue)
    {
        _originalPath = originalPath;
        ParentDirectory = parent;
        Extension = extension;
        IsExecutable = isExecutable;
    }

    public override void Save()
    {
        if (File.Exists(AbsolutePath))
            return;

        var file = File.Create(AbsolutePath);
        file.Close();
        File.WriteAllBytes(AbsolutePath, File.ReadAllBytes(_originalPath));
    }

    public override void Remove()
    {
        File.Delete(AbsolutePath);
    }

    public void CopyTo(Folder folder)
    {
        ParentDirectory = folder;
        folder.AddFile(this);
        folder.Save();
    }

    private int GetSizeInBytes()
    {
        var length = File.ReadAllBytes(AbsolutePath).Length;
        if (length == 0)
            return File.ReadAllBytes(_originalPath).Length;

        return length;
    }

    protected override string GetPath()
    {
        var folders = ParentDirectory.AbsolutePath;
        var path = Path.Combine(folders, $"/{Name}.{Extension}");

        return path;
    }

    protected override bool IsExists()
    {
        var path = GetPath();
        return File.Exists(path);
    }
}