namespace Domain.Item;

public interface IItemRepository
{
    ValueTask<Item> GetById(Guid id);
    ItemId CreateItemId();
}

