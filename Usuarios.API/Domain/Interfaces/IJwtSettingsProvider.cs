namespace Usuarios.API.Domain.Interfaces;

public interface IJwtSettingsProvider
{
    string SecretKey { get; }
}
