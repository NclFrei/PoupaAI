namespace Financias.API.Interfaces;

public interface IJwtSettingsProvider
{
    string SecretKey { get; }
}
