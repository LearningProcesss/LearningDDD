using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Domain.Exceptions;
public class EntityInvalidStateException : Exception
{
    public EntityInvalidStateException()
    {
    }

    public EntityInvalidStateException(string? message) : base(message)
    {
    }

    public EntityInvalidStateException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
    
    public static void ThrowIfNull([NotNull]object? input, [CallerArgumentExpression("argument")] string? name = null)
    {
        if(input is null)
        {
            throw new EntityInvalidStateException();
        }
    }

    public static void ThrowIfNullOrEmptyOrWhiteSpace([NotNull]string? input, [CallerArgumentExpression("argument")] string? name = null)
    {
        if(string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
        {
            throw new EntityInvalidStateException();
        }
    }
}