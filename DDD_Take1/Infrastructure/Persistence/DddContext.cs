using Microsoft.EntityFrameworkCore;

public class DddContext : DbContext
{
    public DbSet<TicketModel> Tickets { get; set; }

    public DddContext(DbContextOptions<DddContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=./Persistence/Data/DddData.db");
        }

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration<TicketModel>(new TicketTypeConfiguation());
    }
}