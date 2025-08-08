using Financias.API.Domain.Interfaces;
using Microsoft.Extensions.Options;

namespace Financias.API.Infrastructure.Configuration;

public class JwtSettingsProvider : IJwtSettingsProvider
{
    private readonly JwtSettings _jwtSettings;

    public JwtSettingsProvider(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public string SecretKey => _jwtSettings.SecretKey;

}
