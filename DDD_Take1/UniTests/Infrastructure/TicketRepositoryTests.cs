using Microsoft.EntityFrameworkCore;

public class TicketRepositoryTests : IClassFixture<DataContextInMemoryClassFixture>
{
    private readonly DataContextInMemoryClassFixture memoryContextFixture;

    public TicketRepositoryTests(DataContextInMemoryClassFixture memoryContextFixture)
    {
        this.memoryContextFixture = memoryContextFixture;
    }

    [Fact]
    public async Task GuardAgainstNullParameter()
    {
        ITicketRepository sut = new TicketRepository(memoryContextFixture.DddContext);

        await Assert.ThrowsAsync<ArgumentNullException>(() => sut.Create(null));
    }

    [Fact]
    public async Task CreateValidPersistenceModel()
    {
        ITicketRepository sut = new TicketRepository(memoryContextFixture.DddContext);

        Guid id = sut.NextId();

        TicketAggregate aggregate = new TicketAggregate(id, "Title", TicketStatus.Assigned, TicketSeverity.Blocking, new[] { new CommentEntity(sut.NextId(), "comment") });

        await sut.Create(aggregate);

        TicketModel? ticketModel = await memoryContextFixture.DddContext.Tickets.Where(model => model.Id.ToString() == id.ToString()).FirstOrDefaultAsync();

        Assert.NotNull(ticketModel);
    }
}