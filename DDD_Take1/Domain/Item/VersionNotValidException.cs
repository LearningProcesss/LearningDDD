public class VersionNotValidDomainException : Exception
{
    public VersionNotValidDomainException()
    {
    }

    public VersionNotValidDomainException(string? message) : base(message)
    {
    }

    public VersionNotValidDomainException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}