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
    
    public static void ThrowIfNull([NotNull]string? argument, [CallerArgumentExpression("argument")] string? paramName = null)
    {
        if(argument is null)
        {
            throw new EntityInvalidStateException();
        }
    }

    public static void ThrowIfNullOrEmptyOrWhiteSpace([NotNull]string? argument, [CallerArgumentExpression("argument")] string? paramName = null)
    {
        if(string.IsNullOrEmpty(argument) || string.IsNullOrWhiteSpace(argument))
        {
            throw new EntityInvalidStateException();
        }
    }

    public static void ThrowIfEmpty([NotNull]Guid argument, [CallerArgumentExpression("argument")] string? paramName = null)
    {
        if(argument == Guid.Empty)
        {
            throw new EntityInvalidStateException();
        }
    }
}