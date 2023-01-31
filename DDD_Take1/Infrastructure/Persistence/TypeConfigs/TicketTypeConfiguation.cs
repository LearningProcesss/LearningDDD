using Microsoft.EntityFrameworkCore;

public class TicketTypeConfiguation : IEntityTypeConfiguration<TicketModel>
{
    public void Configure(EntityTypeBuilder<TicketModel> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Title).IsRequired().HasMaxLength(200);
        builder.Property(p => p.Status).IsRequired().HasConversion<string>();
        builder.Property(p => p.Severity).IsRequired().HasConversion<string>();

        builder.HasMany(ticket => ticket.Comments).WithOne(comment => comment.Ticket).HasForeignKey(p => p.TicketId);
    }
}