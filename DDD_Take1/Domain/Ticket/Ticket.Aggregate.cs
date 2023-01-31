public class TicketAggregate
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public TicketStatus Status { get; private set; }
    public TicketSeverity Severity { get; private set; }
    public List<CommentEntity> Comments { get; private set; }

    public TicketAggregate(Guid id, string title, TicketStatus status, TicketSeverity severity)
    {
        if (id == Guid.Empty)
        {
            throw new EntityInvalidStateException($"{nameof(id)} Empty Guid not permitted.");
        }
        EntityInvalidStateException.ThrowIfNull(id, nameof(id));
        EntityInvalidStateException.ThrowIfNullOrEmptyOrWhiteSpace(title, nameof(title));
        this.Id = id;
        this.Title = title;
        this.Status = status;
        this.Severity = severity;
        // this.Comments = comments;
    }
}