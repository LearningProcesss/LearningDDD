public class Item
{
    public ItemId Id { get; private set; }
    public VersionValueObject Version { get; private set; }
    public string Classification { get; private set; }
    public int Status { get; private set; }
    public List<ItemId> Structure { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    public Item(ItemId id, string classification, VersionValueObject version, int status, string name, string description)
    {
        ArgumentNullException.ThrowIfNull(id);

        Id = id;
        Version = version;
        ChangeClassification(classification);
        ChangeStatus(status);
        ChangeName(name);
    }

    public void ChangeClassification(string classification)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(classification, nameof(classification));

        Classification = classification;
    }

    public void ChangeStatus(int status)
    {
        if(status <= 0)
        {
            
        }

        Status = status;
    }

    public void ChangeName(string name)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(name, nameof(name));

        Name = name; 
    }

    public void ChangeDescription(string description)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(description, nameof(description));

        Description = description;
    }

    public void AddStructureItem(ItemId id)
    {
        ArgumentNullException.ThrowIfNull(id, nameof(id));

        Structure.Add(id);
    }
}