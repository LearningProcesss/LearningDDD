public class ItemRepository : IItemRepository
{
    public ItemRepository()
    {
        
    }

    public ItemId CreateItemId()
    {
        return new ItemId(Guid.NewGuid());
    }

    ValueTask<Domain.Item.Item> IItemRepository.GetById(Guid id)
    {
        throw new NotImplementedException();
    }
}