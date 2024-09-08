namespace Identity.Domain.Primitives;

public interface IAuditableData
{
    public DateTimeOffset CreatedAt { get; protected set; }
}