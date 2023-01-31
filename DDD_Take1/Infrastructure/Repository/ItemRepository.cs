public class ItemRepository : IItemRepository
{
    public ItemRepository()
    {
        
    }

    public Task<Item> GetById(Guid id)
    {
        throw new NotImplementedException();
    }
}