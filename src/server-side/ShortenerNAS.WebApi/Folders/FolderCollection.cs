using System.Collections;

namespace ShortenerNAS.WebApi.Folders;

public class FolderCollection : ICollection<Folder>, IReadOnlyCollection<Folder>
{
    private readonly List<Folder> _files = new List<Folder>();

    public int Count => _files.Count;
    public bool IsReadOnly => false;

    public IEnumerator<Folder> GetEnumerator()
    {
        return _files.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(Folder item)
    {
        _files.Add(item);
    }

    public void AddRange(IEnumerable<Folder> items)
    {
        _files.AddRange(items);
    }

    public void Clear()
    {
        _files.Clear();
    }

    public bool Contains(Folder item)
    {
        return _files.Contains(item);
    }

    public void CopyTo(Folder[] array, int arrayIndex)
    {
        foreach (var i in _files)
        {
            array.SetValue(i, arrayIndex);
            arrayIndex += 1;
        }
    }

    public bool Remove(Folder item)
    {
        return _files.Remove(item);
    }
}