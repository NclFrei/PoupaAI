using Chatbot.API.Domain.Interface;
using Chatbot.API.Domain.Interfaces;
using Microsoft.Extensions.Options;

namespace Chatbot.API.Infrastructure.Configuration;

public class JwtSettingsProvider : IJwtSettingsProvider
{
    private readonly JwtSettings _jwtSettings;

    public JwtSettingsProvider(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public string SecretKey => _jwtSettings.SecretKey;

}
