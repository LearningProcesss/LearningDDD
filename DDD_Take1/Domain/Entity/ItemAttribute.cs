namespace Domain.Entity;

public sealed class ItemAttribute
{
    private Guid id;
    private Guid itemId;
    private string name;
    private object value;


    public Guid Id => id;
    public Guid ItemId => itemId;
    public string Name => name;
    public object Value => value;

    public ItemAttribute(Guid itemId, string name, object value)
    {
        this.itemId = itemId;
        this.name = name;
        this.value = value;
    }

    public void SetValue(object value)
    {
        ArgumentNullException.ThrowIfNull(value, this.Name);

        this.value = value;
    }
}