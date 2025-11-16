using ShortenerNAS.WebApi.Common.Domain;

namespace ShortenerNAS.WebApi.Folders;

public class Group(string name) : ValueObject
{
    public string Name => name;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }
}