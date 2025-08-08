using Microsoft.Extensions.Options;
using Usuarios.API.Domain.Interfaces;

namespace Usuarios.API.Infrastructure.Configuration;

public class JwtSettingsProvider : IJwtSettingsProvider
{
    private readonly JwtSettings _jwtSettings;

    public JwtSettingsProvider(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public string SecretKey => _jwtSettings.SecretKey;

}

