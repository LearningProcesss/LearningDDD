namespace Domain.Item;

public interface IItemRepository
{
    Task<Item> GetById(Guid id);
}

