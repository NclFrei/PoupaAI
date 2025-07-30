

namespace Usuarios.Domain.ExceptionsBase;

public class ErrorOnValidationException : UsuarioException
{
    public IList<string> ErrorMessages { get; set; }

    public ErrorOnValidationException(IList<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }
}
