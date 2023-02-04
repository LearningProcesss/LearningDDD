public class VersionValueObjectTests
{
    public VersionValueObjectTests()
    {
        
    }

    [Fact]
    public void GetCorrectVersionIndex()
    {
        Guid itemVer1 = Guid.NewGuid();

        Guid itemVer2 = Guid.NewGuid();

        Guid itemVer3 = Guid.NewGuid();

        var history = new [] 
        { 
            new Version(new ItemId(itemVer1), 1, 1, new ItemId(itemVer1)),
            new Version(new ItemId(itemVer2), 2, 1, new ItemId(itemVer1)),
            new Version(new ItemId(itemVer3), 3, 1, new ItemId(itemVer2))
        };

        VersionHistory sut = new(new ItemId(itemVer1), history);

        Version version = sut.CurrentVersion();

        Version expected = new Version(new ItemId(itemVer1), 1, 1, new ItemId(itemVer1));

        Assert.True(expected == version);


        // VersionValueObject firstVersion = new VersionValueObject(1, 1, 0, 0, null);

        // VersionValueObject secondVersion = new VersionValueObject(2, 1, 0, 0, firstVersion);

        // VersionValueObject actualVersion = new VersionValueObject(3, 1, 0, 0, secondVersion);

        // Assert.Equal(3, actualVersion.ActualVersionIndex());
    }
}