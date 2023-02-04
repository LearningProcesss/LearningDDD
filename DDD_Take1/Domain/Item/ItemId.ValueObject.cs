public class ItemId : ValueObject
{
    public Guid Id { get; private set; }

    public ItemId(Guid id)
    {
        this.Id = id;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }
}