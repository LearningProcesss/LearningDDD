public class VersionValueObjectTests
{
    public VersionValueObjectTests()
    {
        
    }

    [Fact]
    public void GetCorrectVersionIndex()
    {
        VersionValueObject firstVersion = new VersionValueObject(1, 1, 0, 0, null);

        VersionValueObject secondVersion = new VersionValueObject(2, 1, 0, 0, firstVersion);

        VersionValueObject actualVersion = new VersionValueObject(3, 1, 0, 0, secondVersion);

        Assert.Equal(3, actualVersion.ActualVersionIndex());
    }
}