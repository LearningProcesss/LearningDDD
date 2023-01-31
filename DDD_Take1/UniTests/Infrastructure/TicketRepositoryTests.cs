using Microsoft.EntityFrameworkCore;

public class TicketRepositoryTests : IClassFixture<DataContextInMemoryClassFixture>
{
    private readonly DataContextInMemoryClassFixture memoryContextFixture;

    public TicketRepositoryTests(DataContextInMemoryClassFixture memoryContextFixture)
    {
        this.memoryContextFixture = memoryContextFixture;
    }

    [Fact]
    public async Task CreateValidPersistenceModel()
    {
        Guid id = Guid.NewGuid();

        TicketAggregate aggregate = new TicketAggregate(id, "Title", TicketStatus.Assigned, TicketSeverity.Blocking);

        ITicketRepository sut = new TicketRepository(memoryContextFixture.DddContext);

        await sut.Create(aggregate);

        TicketModel? ticketModel = await memoryContextFixture.DddContext.Tickets.Where(model => model.Id.ToString() == id.ToString()).FirstOrDefaultAsync();

        Assert.NotNull(ticketModel);
    }
}