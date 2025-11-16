using System.Collections;

namespace ShortenerNAS.WebApi.Folders;

public class FileCollection : ICollection<FolderFile>, IReadOnlyCollection<FolderFile>
{
    private readonly List<FolderFile> _files = new List<FolderFile>();

    public int Count => _files.Count;
    public bool IsReadOnly => false;

    public IEnumerator<FolderFile> GetEnumerator()
    {
        return _files.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(FolderFile item)
    {
        _files.Add(item);
    }

    public void AddRange(IEnumerable<FolderFile> items)
    {
        _files.AddRange(items);
    }

    public void Clear()
    {
        _files.Clear();
    }

    public bool Contains(FolderFile item)
    {
        return _files.Contains(item);
    }

    public void CopyTo(FolderFile[] array, int arrayIndex)
    {
        foreach (var i in _files)
        {
            array.SetValue(i, arrayIndex);
            arrayIndex += 1;
        }
    }

    public bool Remove(FolderFile item)
    {
        return _files.Remove(item);
    }
}