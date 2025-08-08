using Usuarios.API.Domain.Models;

namespace Usuarios.API.Infrastructure.RabbitMqClient;

public interface IRabbitMqClient
{
    void PublicaUsuarioCriado(Usuario usuarioCriado);
}
