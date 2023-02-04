public class StructureItemAlreadyExistsDomainException : Exception
{
    public StructureItemAlreadyExistsDomainException()
    {
    }

    public StructureItemAlreadyExistsDomainException(string? message) : base(message)
    {
    }

    public StructureItemAlreadyExistsDomainException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}