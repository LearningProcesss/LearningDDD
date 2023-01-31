namespace Domain.Item;

public class Item
{
    private Guid id;
    private DateTime createdOn;
    private DateTime updatedOn;
    private List<ItemAttribute> attributes;

    public Guid Id => id;
    public DateTime UpdatedOn => updatedOn;
    public DateTime CreatedOn => createdOn;

    public Item(DateTime createdOn, DateTime updatedOn, List<ItemAttribute> attributes)
    {
        this.createdOn = createdOn;
        this.updatedOn = updatedOn;
        this.attributes = attributes;
    }

    public void AddAttribute(string attributeName, object attributeValue)
    {
        if(attributes.Any(item => item.Name.ToLower() == attributeName.ToLower()))
        {
            throw new EntityInvalidStateException($"Item already contains attribute {attributeName}");
        }

        attributes.Add(new ItemAttribute(id, attributeName, attributeValue));
    }
}