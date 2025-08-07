namespace Usuarios.API.Interfaces;

public interface IJwtSettingsProvider
{
    string SecretKey { get; }
}
