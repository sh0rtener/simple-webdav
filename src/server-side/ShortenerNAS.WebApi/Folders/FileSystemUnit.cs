using ShortenerNAS.WebApi.Common.Domain;

namespace ShortenerNAS.WebApi.Folders;

public abstract class FileSystemUnit : Entity<Guid>
{
    public string Name { get; private set; }
    public string AbsolutePath => GetPath();
    public ObjectOwnersValue Owners { get; private set; }
    public ObjectAccessValue Access { get; private set; }

    public FileSystemUnit(string name, ObjectOwnersValue owners, ObjectAccessValue access)
    {
        Name = name;
        Owners = owners;
        Access = access;
    }
    protected abstract string GetPath();
    protected abstract bool IsExists();

    public abstract void Save();

    public abstract void Remove();
}