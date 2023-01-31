public class CreateTicketUseCase : IUseCase<CreateTicketCommand, TicketDto>
{
    private readonly ITicketRepository ticketRepository;

    public CreateTicketUseCase(ITicketRepository ticketRepository)
    {
        this.ticketRepository = ticketRepository;
    }
    public async Task<TicketDto> Handle(CreateTicketCommand input)
    {
        // a service that create guid

        Guid guid = Guid.NewGuid();

        TicketAggregate entity = new(guid, input.Title, input.Status, input.Severity);

        await ticketRepository.Create(entity);

        return new TicketDto(guid, entity.Title, entity.Status, entity.Severity);
    }
}