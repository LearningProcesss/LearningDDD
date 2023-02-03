public class ItemId
{
    public Guid Id { get; private set; }

    public ItemId(Guid id)
    {
        this.Id = id;
    }
}