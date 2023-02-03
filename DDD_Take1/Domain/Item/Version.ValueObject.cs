public class VersionValueObject : ValueObject
{
    public int Version { get; }
    public int Revision { get; }
    public int VersionIndex { get; }
    public int VersionCount { get; }
    public VersionValueObject Ancestor { get; }

    public VersionValueObject(int version, int revision, int versionIndex, int versionCount, VersionValueObject ancestor) : base()
    {
        EnsureInvariant(version, revision, versionIndex, versionCount);
        Version = version;
        Revision = revision;
        VersionIndex = versionIndex;
        VersionCount = versionCount;
        Ancestor = ancestor;
    }

    private void EnsureInvariant(int version, int revision, int versionIndex, int versionCount)
    {
        if (version < 1 && revision < 1)
        {
            throw new VersionNotValidDomainException("Version and Revision can't be lower than 1");
        }

        // if (versionIndex < 1)
        // {
        //     throw new VersionNotValidDomainException("VersionIndex can't be lower than 1");
        // }

        // if (versionCount < 1)
        // {
        //     throw new VersionNotValidDomainException("VersionCount can't be lower than 1");
        // }
    }

    public int ActualVersionIndex()
    {
         return Ancestor is null ? 1 : Ancestor.ActualVersionIndex() + 1;
    }

    public int ActualVersionCount()
    {
        return Ancestor is null ? 1 : Ancestor.ActualVersionCount() + 1;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Version;
        yield return Revision;
        // yield return VersionHistory.Count();
    }
}

public abstract class ValueObject
{
    protected static bool EqualOperator(ValueObject left, ValueObject right)
    {
        if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
        {
            return false;
        }
        return ReferenceEquals(left, right) || left.Equals(right);
    }

    protected static bool NotEqualOperator(ValueObject left, ValueObject right)
    {
        return !(EqualOperator(left, right));
    }

    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (ValueObject)obj;

        return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    }

    public static bool operator ==(ValueObject one, ValueObject two)
    {
        return EqualOperator(one, two);
    }

    public static bool operator !=(ValueObject one, ValueObject two)
    {
        return NotEqualOperator(one, two);
    }
}