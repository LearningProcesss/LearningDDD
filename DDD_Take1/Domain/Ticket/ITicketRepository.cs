public interface ITicketRepository
{
    Task Create(TicketAggregate aggregate);
    Guid NextId();
}