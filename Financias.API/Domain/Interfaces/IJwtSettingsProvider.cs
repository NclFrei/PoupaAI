namespace Financias.API.Domain.Interfaces;

public interface IJwtSettingsProvider
{
    string SecretKey { get; }
}
