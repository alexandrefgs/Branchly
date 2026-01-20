namespace Branchly.Domain.Abstractions;

public abstract class Entity
{
    public Guid Id { get; protected set; }

    protected Entity()
    {
        // Necessário para EF Core
    }

    protected Entity(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id não pode ser vazio.");

        Id = id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity other)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (GetType() != other.GetType())
            return false;

        return Id == other.Id;
    }

    public override int GetHashCode()
        => HashCode.Combine(GetType(), Id);
}
