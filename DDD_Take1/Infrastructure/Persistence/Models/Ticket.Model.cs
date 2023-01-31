public class TicketModel
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public TicketStatus Status { get; set; }
    public TicketSeverity Severity { get; set; }
}