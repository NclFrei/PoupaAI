using Microsoft.Extensions.Options;
using Usuarios.API.Interfaces;

namespace Usuarios.API.Configuration;

public class JwtSettingsProvider : IJwtSettingsProvider
{
    private readonly JWTSettings _jwtSettings;

    public JwtSettingsProvider(IOptions<JWTSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public string SecretKey => _jwtSettings.SecretKey;

}

