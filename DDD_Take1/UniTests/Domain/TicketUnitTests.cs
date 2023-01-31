using Domain.Exceptions;

public class TicketUnitTests
{
    public TicketUnitTests()
    {

    }

    [Theory, MemberData(nameof(ValidEntityParameters))]
    public void ThrowExceptionWhenParametersAreNotValid(Guid id, string title, TicketStatus status, TicketSeverity severity, IEnumerable<CommentEntity> comments)
    {
        Assert.Throws<EntityInvalidStateException>(() => new TicketAggregate(id, title, status, severity, comments));
    }

    public static IEnumerable<object[]> ValidEntityParameters
    {
        get
        {
            yield return new object[] { Guid.NewGuid(), string.Empty, TicketStatus.Assigned, TicketSeverity.Blocking, new[] { new CommentEntity(Guid.NewGuid(), "comment") } };
            yield return new object[] { Guid.NewGuid(), null, TicketStatus.Assigned, TicketSeverity.Blocking, new[] { new CommentEntity(Guid.NewGuid(), "comment") } };
            yield return new object[] { Guid.NewGuid(), " ", TicketStatus.Assigned, TicketSeverity.Blocking, new[] { new CommentEntity(Guid.NewGuid(), "comment") } };
            yield return new object[] { Guid.Empty, "Title", TicketStatus.Assigned, TicketSeverity.Blocking, new[] { new CommentEntity(Guid.NewGuid(), "comment") } };

        }
    }
}