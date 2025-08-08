using Financias.API.Domain.Interfaces;
using Microsoft.Extensions.Options;

namespace Financias.API.Infrastructure.Configuration;

public class JwtSettingsProvider : IJwtSettingsProvider
{
    private readonly JWTSettings _jwtSettings;

    public JwtSettingsProvider(IOptions<JWTSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public string SecretKey => _jwtSettings.SecretKey;

}
