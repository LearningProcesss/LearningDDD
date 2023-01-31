using Microsoft.EntityFrameworkCore;

public class CommentTypeConfiguation : IEntityTypeConfiguration<CommentModel>
{
    public void Configure(EntityTypeBuilder<CommentModel> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Comment).IsRequired().HasMaxLength(500);
    }
}