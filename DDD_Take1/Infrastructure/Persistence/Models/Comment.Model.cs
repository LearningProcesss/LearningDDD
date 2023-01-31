public class CommentModel
{
    public Guid Id { get; set; }
    public string Comment { get; set; }
    public Guid TicketId { get; set; }
    public TicketModel Ticket { get; set; }
}