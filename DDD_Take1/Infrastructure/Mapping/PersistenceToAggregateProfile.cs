public class PersistenceToAggregateProfile : Profile
{
    public PersistenceToAggregateProfile()
    {
        CreateMap<TicketModel, TicketAggregate>().ConstructUsing(model => new TicketAggregate(
            model.Id,
            model.Title,
            model.Status,
            model.Severity
        ));
        CreateMap<TicketAggregate, TicketModel>();
    }
}