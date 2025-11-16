using ShortenerNAS.WebApi.Common.Domain;

namespace ShortenerNAS.WebApi.Folders;

public class ObjectOwnersValue : ValueObject
{
    private string _creator;
    private string _owner;
    private string _group;

    public string Creator => _creator;
    public string Owner => _owner;
    public string Group => _group;

    public ObjectOwnersValue(string creator, string owner, string group)
    {
        _creator = creator;
        _owner = owner;
        _group = group;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Creator;
        yield return Owner;
        yield return Group;     
    }
}