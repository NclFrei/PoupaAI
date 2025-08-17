using Usuarios.API.Domain.DTOs.Response;
using Usuarios.API.Domain.Models;

namespace Usuarios.API.Infrastructure.RabbitMqClient;

public interface IRabbitMqClient
{
    void PublicaUsuario(UsuarioResponseEvent usuarioCriado);
}
