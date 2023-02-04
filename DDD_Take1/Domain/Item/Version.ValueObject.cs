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

public sealed class VersionHistory : ValueObject
{
    private readonly ItemId currentId;
    private readonly IEnumerable<Version> versions;

    public VersionHistory(ItemId currentId, IEnumerable<Version> versions)
    {
        ArgumentValidationDomainException.ThrowIfNull(currentId);
        ArgumentValidationDomainException.ThrowIfNull(versions);
        this.currentId = currentId;
        this.versions = versions;
    }

    public Version CurrentVersion()
    {
        return versions.First(history => history.ItemId == currentId);
    } 

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return versions.Count();
    }
}

public sealed class Version : ValueObject
{
    public ItemId ItemId { get; private set; }
    public int Ver { get; private set; }
    public int Rev { get; private set; }
    public ItemId Ancestor { get; private set; }

    public Version(ItemId itemId, int ver, int rev, ItemId ancestor)
    {
        EnsureInvariant(itemId, ver, rev, ancestor);
        this.ItemId = itemId;
        this.Ver = ver;
        this.Rev = rev;
        this.Ancestor = ancestor;
    }

    private void EnsureInvariant(ItemId itemId, int version, int revision, ItemId ancestor)
    {
        if (version <= 0)
        {
            throw new VersionNotValidDomainException("Version can't be lower than 1");
        }

        if (revision <= 0)
        {
            throw new VersionNotValidDomainException("Revision can't be lower than 1");
        }
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ItemId;
        yield return Ancestor;
        yield return Ver;
        yield return Rev;
        yield return Ancestor;
    }
}