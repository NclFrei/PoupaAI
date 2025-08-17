
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using Usuarios.API.Domain.DTOs.Response;
using Usuarios.API.Domain.Models;

namespace Usuarios.API.Infrastructure.RabbitMqClient;

public class RabbitMqClient : IRabbitMqClient
{

    private readonly IConfiguration _configuration;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMqClient(IConfiguration configuration)
    {
        _configuration = configuration;
        _connection = new ConnectionFactory() { HostName = _configuration["RabbitMqHost"], Port = Int32.Parse( _configuration["RabbitMqPort"]) }.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
    }

    public void PublicaUsuarioCriado(UsuarioResponseEvent usuarioCriado)
    {
        var message = JsonSerializer.Serialize(usuarioCriado);
        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(exchange: "trigger", routingKey: "", basicProperties: null, body: body);
    }
    
    public void PublicaUsuario(UsuarioResponseEvent usuarioAtualizado)
    {
        var message = JsonSerializer.Serialize(usuarioAtualizado);
        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(exchange: "trigger", routingKey: "", basicProperties: null, body: body);
    }
    
}
