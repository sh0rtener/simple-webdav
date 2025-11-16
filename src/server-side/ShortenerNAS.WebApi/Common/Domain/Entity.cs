namespace ShortenerNAS.WebApi.Common.Domain;

public abstract class Entity<TId>
{
    public TId? Id { get; set; }
}