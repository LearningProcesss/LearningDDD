public abstract class Validator
{
    private readonly IValidationNotificationHandler handler;

    public Validator(IValidationNotificationHandler handler)
    {
        this.handler = handler ?? throw new ArgumentNullException(nameof(handler));
    }

    public abstract void Validate();

    protected IValidationNotificationHandler Handler()
    {
        return this.handler;
    }
}

public class TicketValidator : Validator
{
    private readonly TicketAggregate ticket;

    public TicketValidator(TicketAggregate ticket, IValidationNotificationHandler handler) : base(handler)
    {
        this.ticket = ticket;
    }

    public override void Validate()
    {
        if(ticket.Status == TicketStatus.Closed)
        {
            Handler().HandleError("Fake error");
        }
    }
}

public interface IValidationNotificationHandler
{
    void HandleError(string error);
}

public class TicketValidationNotificationHandler : IValidationNotificationHandler
{
    public TicketValidationNotificationHandler()
    {
        
    }

    public void HandleError(string error)
    {
        
    }
}