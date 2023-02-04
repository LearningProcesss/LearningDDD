public class Item
{
    public ItemId Id { get; private set; }
    public VersionHistory VersionHistory { get; private set; }
    public string Classification { get; private set; }
    public int Status { get; private set; }
    public List<ItemId> Structure { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    public Item(ItemId id, string classification, VersionHistory versionHistory, int status, string name, string description, IEnumerable<ItemId> structure)
    {
        ArgumentValidationDomainException.ThrowIfNull(id);
        ArgumentValidationDomainException.ThrowIfNull(versionHistory);

        Id = id;
        VersionHistory = versionHistory;
        ChangeClassification(classification);
        ChangeStatus(status);
        ChangeName(name);
        Structure = structure?.ToList() ?? new List<ItemId>();
    }

    public void ChangeClassification(string classification)
    {
        ArgumentValidationDomainException.ThrowIfNullOrEmpty(classification, nameof(classification));

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
        ArgumentValidationDomainException.ThrowIfNullOrEmpty(name, nameof(name));

        Name = name; 
    }

    public void ChangeDescription(string description)
    {
        ArgumentValidationDomainException.ThrowIfNullOrEmpty(description, nameof(description));

        Description = description;
    }

    public void AddStructureItem(ItemId id)
    {
        ArgumentValidationDomainException.ThrowIfNull(id, nameof(id));

        if(Structure.Any(item => item == id))
        {
            throw new StructureItemAlreadyExistsDomainException($"Item: {Id.Id} structure already contains {id.Id}");
        }

        Structure.Add(id);
    }
}