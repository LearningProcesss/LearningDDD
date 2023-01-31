public class TicketRepository : ITicketRepository
{
    private readonly DddContext context;
    private readonly IMapper mapper;

    public TicketRepository(DddContext context)
    {
        ArgumentNullException.ThrowIfNull(context, nameof(context));
        
        this.context = context;
    }
    public async Task Create(TicketAggregate aggregate)
    {
        // automapper hides Domain Entity construction. But i'm using constructor and not property mapping. 
        // So valid entity rules by constructor should apply anyway.
        // TicketModel ticketModel = mapper.Map<TicketModel>(aggregate);

        TicketModel model = new TicketModel
        {
            Id = aggregate.Id,
            Title = aggregate.Title,
            Status = aggregate.Status,
            Severity = aggregate.Severity
        };

        await context.Tickets.AddAsync(model);

        await context.SaveChangesAsync();
    }
}