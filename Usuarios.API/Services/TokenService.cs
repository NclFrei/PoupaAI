using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Usuarios.API.Interfaces;
using Usuarios.API.Models;

namespace Usuarios.Application.Services;

public class TokenService
{
    private readonly IJwtSettingsProvider _jwtSettingsProvider;

    public TokenService(IJwtSettingsProvider jwtSettingsProvider)
    {
        _jwtSettingsProvider = jwtSettingsProvider;
    }

    public string Generate(Usuario usuario)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtSettingsProvider.SecretKey);

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256
        );

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(usuario),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = credentials
        };

        var token = handler.CreateToken(tokenDescriptor);

        return handler.WriteToken(token);
    }


    public static ClaimsIdentity GenerateClaims(Usuario user)
    {
        var ci = new ClaimsIdentity();

        ci.AddClaim(
            new Claim(ClaimTypes.Name, user.Email));

        return ci;
    }
}
