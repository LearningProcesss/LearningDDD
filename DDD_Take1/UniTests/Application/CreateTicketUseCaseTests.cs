public class CreateTicketUseCaseTests : IClassFixture<DataContextInMemoryClassFixture>
{
    private readonly DataContextInMemoryClassFixture memoryContextFixture;

    public CreateTicketUseCaseTests(DataContextInMemoryClassFixture memoryContextFixture)
    {
        this.memoryContextFixture = memoryContextFixture;
    }

    [Fact]
    public async Task CreateValidAggregateRoot()
    {
        ITicketRepository repository = new TicketRepository(memoryContextFixture.DddContext);

        IUseCase<CreateTicketCommand, TicketDto> sut = new CreateTicketUseCase(repository);

        TicketDto ticketDto = await sut.Handle(new CreateTicketCommand("Title", TicketStatus.Assigned, TicketSeverity.Blocking, "comment"));

        Assert.NotNull(ticketDto);
    }
}