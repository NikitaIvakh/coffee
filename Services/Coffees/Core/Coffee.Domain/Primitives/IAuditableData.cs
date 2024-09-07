namespace Coffee.Domain.Primitives;

public interface IAuditableData
{
    public DateTimeOffset CreatedAt { get; set; }
}