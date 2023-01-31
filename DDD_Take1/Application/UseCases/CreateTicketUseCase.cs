public class CreateTicketUseCase : IUseCase<CreateTicketCommand, TicketDto>
{
    private readonly ITicketRepository ticketRepository;

    public CreateTicketUseCase(ITicketRepository ticketRepository)
    {
        this.ticketRepository = ticketRepository;
    }
    public async Task<TicketDto> Handle(CreateTicketCommand input)
    {
        TicketAggregate entity = new(ticketRepository.NextId(),
                                     input.Title,
                                     input.Status,
                                     input.Severity,
                                     new[] 
                                    { 
                                        new CommentEntity(ticketRepository.NextId(), input.Comment) 
                                    });

        await ticketRepository.Create(entity);

        return new TicketDto(entity.Id, entity.Title, entity.Status, entity.Severity);
    }
}