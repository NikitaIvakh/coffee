namespace Coffee.Domain.Primitives;

public abstract class Entity : IEquatable<Entity>
{
    protected Entity() {}

    protected Entity(Guid id) => Id = id;
    
    private Guid Id { get; }

    public static bool operator ==(Entity? left, Entity? right)
    {
        if (left is null && right is null)
            return false;

        if (left is null || right is null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(Entity? left, Entity? right)
    {
        return !(left == right);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity entity)
            return false;

        return entity.GetType() == GetType() && Equals(entity);
    }

    public bool Equals(Entity? other)
    {
        if (other is null)
            return false;

        if (GetType() != other.GetType())
            return false;

        return other.Id == Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id.GetHashCode());
    }
}