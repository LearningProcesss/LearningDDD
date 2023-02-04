public class ItemTests
{
    public ItemTests()
    {

    }

    [Fact]
    public void CreateValiditemEntity()
    {
        Guid guid = Guid.NewGuid();

        ItemId itemId = new ItemId(guid);

        // VersionValueObject one = new VersionValueObject(1, 1, 1, 1, null);

        // VersionValueObject two = new VersionValueObject(2, 1, 1, 1, one);

        // VersionValueObject three = new VersionValueObject(3, 1, 1, 1, two);

        Guid itemVer1 = Guid.NewGuid();

        Guid itemVer2 = Guid.NewGuid();

        Guid itemVer3 = Guid.NewGuid();

        var history = new [] 
        { 
            new Version(new ItemId(itemVer1), 1, 1, new ItemId(itemVer1)),
            new Version(new ItemId(itemVer2), 2, 1, new ItemId(itemVer1)),
            new Version(new ItemId(itemVer3), 3, 1, new ItemId(itemVer2))
        };

        VersionHistory versionHistory = new(itemId, history);

        Item sut = new Item(itemId, "CAD-2D", versionHistory, 100, "test item", "this is the description for a test item", new [] { new ItemId(Guid.NewGuid()) });

        Assert.NotNull(sut);
    }
}