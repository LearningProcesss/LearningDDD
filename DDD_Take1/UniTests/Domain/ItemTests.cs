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

        VersionValueObject one = new VersionValueObject(1, 1, 1, 1, null);

        VersionValueObject two = new VersionValueObject(2, 1, 1, 1, one);

        VersionValueObject three = new VersionValueObject(3, 1, 1, 1, two);

        Item sut = new Item(itemId, "CAD-2D", three, 100, "test item", "this is the description for a test item");

        Assert.NotNull(sut);
    }
}