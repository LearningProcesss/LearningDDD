public class ArgumentValidationDomainException : ArgumentNullException
{
    public ArgumentValidationDomainException()
    {
    }

    public ArgumentValidationDomainException(string? message) : base(message)
    {
    }

    public ArgumentValidationDomainException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    // public static void ThrowIfNull()
    // {

    // }

    // public static void ThrowIfNullOrEmpty()
    // {

    // }
}