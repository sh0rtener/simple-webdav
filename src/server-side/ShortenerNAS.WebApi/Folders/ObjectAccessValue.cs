using ShortenerNAS.WebApi.Common.Domain;

namespace ShortenerNAS.WebApi.Folders;

public class ObjectAccessValue : ValueObject
{
    private ObjectAccess _ownerAccess;
    private ObjectAccess _groupAccess;
    private ObjectAccess _everybodyAccess;

    public ObjectAccess OwnerAccess => _ownerAccess;
    public ObjectAccess GroupAccess => _groupAccess;
    public ObjectAccess EverybodyAccess => _everybodyAccess;

    public ObjectAccessValue(ObjectAccess ownerAccess, ObjectAccess groupAccess, ObjectAccess everybodyAccess)
    {
        _ownerAccess = ownerAccess;
        _groupAccess = groupAccess;
        _everybodyAccess = everybodyAccess;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return OwnerAccess;
        yield return GroupAccess;
        yield return EverybodyAccess;
    }
}