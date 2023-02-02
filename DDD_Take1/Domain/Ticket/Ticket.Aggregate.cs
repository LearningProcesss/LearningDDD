public class TicketAggregate
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public TicketStatus Status { get; private set; }
    public TicketSeverity Severity { get; private set; }
    public List<CommentEntity>? Comments { get; private set; }

    public TicketAggregate(Guid id, string title, TicketStatus status, TicketSeverity severity, IEnumerable<CommentEntity>? comments)
    {
        // EnforceInvariant();
        SetId(id);
        SetTitle(title);
        SetStatus(status);
        SetSeverity(severity);

        this.Comments = comments?.ToList() ?? new List<CommentEntity>();
    }

    private void SetId(Guid id)
    {
        EntityInvalidStateException.ThrowIfEmpty(id, nameof(id));

        this.Id = id;
    }

    public void SetTitle(string title)
    {
        EntityInvalidStateException.ThrowIfNullOrEmptyOrWhiteSpace(title, nameof(title));

        this.Title = title;
    }

    public void SetStatus(TicketStatus status)
    {
        this.Status = status;
    }

    public void SetSeverity(TicketSeverity severity)
    {
        this.Severity = severity;
    }
}