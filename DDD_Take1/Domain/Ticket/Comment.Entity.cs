public class CommentEntity
{
    private Guid id;
    private string comment;
    // private DateTime createdOn;

    public CommentEntity(Guid id, string comment)
    {
        this.id = id;
        this.comment = comment;
        // this.createdOn = createdOn;
    }
}