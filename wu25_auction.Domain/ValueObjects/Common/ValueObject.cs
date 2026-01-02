namespace wu25_auction.Domain.ValueObjects.Common;

public abstract class ValueObject : IEquatable<ValueObject>
{
    protected abstract IEnumerable<object> GetEqualityComponents();

    public bool Equals(ValueObject? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (GetType() != other.GetType()) return false;

        return GetEqualityComponents()
            .SequenceEqual(other.GetEqualityComponents());
    }

    public override bool Equals(object? obj)
        => Equals(obj as ValueObject);

    public override int GetHashCode()
        => GetEqualityComponents().Aggregate(1, HashCode.Combine);
    
    public static bool operator ==(ValueObject left, ValueObject right)
        => left.Equals(right);

    public static bool operator !=(ValueObject left, ValueObject right) 
        => !Equals(left, right);
}