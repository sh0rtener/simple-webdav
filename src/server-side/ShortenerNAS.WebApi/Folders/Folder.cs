namespace ShortenerNAS.WebApi.Folders;

public class Folder : FileSystemUnit
{
    private FolderCollection _children = new FolderCollection();
    private FileCollection _files = new FileCollection();

    public Folder(string name, ObjectOwnersValue owners, ObjectAccessValue access, Folder? parent) : base(name, owners,
        access)
    {
        Id = Guid.NewGuid();
        Parent = parent;
    }

    public Folder? Parent { get; private set; }
    public IReadOnlyCollection<Folder> Children => _children;
    public IReadOnlyCollection<FolderFile> Files => _files;

    public void AddFile(FolderFile file)
    {
        _files.Add(file);
    }

    public void AddFiles(IEnumerable<FolderFile> files)
    {
        _files.AddRange(files);
    }

    public void AddFolder(Folder folder)
    {
        _children.Add(folder);
    }

    public void AddFolders(IEnumerable<Folder> folders)
    {
        _children.AddRange(folders);
    }

    public void CopyTo(Folder folder)
    {
        var newFolder = this.MemberwiseClone() as Folder;
        newFolder!.Parent = folder;
        folder.AddFolder(newFolder);
        folder.Save();
        newFolder.Save();
    }

    public override void Save()
    {
        if (!Directory.Exists(AbsolutePath))
            Directory.CreateDirectory(AbsolutePath);

        foreach (var folder in Children)
        {
            folder.Save();
        }

        foreach (var file in _files)
        {
            file.Save();
        }
    }

    public override void Remove()
    {
        if (!Directory.Exists(AbsolutePath))
            throw new DirectoryNotFoundException();
        Directory.Delete(AbsolutePath, true);
    }

    protected override string GetPath()
    {
        var parents = new List<string>();
        var node = Parent;

        while (node != null)
        {
            parents.Add(node.Name);
            node = node.Parent;
        }

        parents.Reverse();
        parents.Add(this.Name);
        var path = string.Join("/", parents);

        return path;
    }

    protected override bool IsExists()
    {
        var path = GetPath();
        return Directory.Exists(path);
    }
}