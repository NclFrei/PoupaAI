using System.Text;
using System.Text.Json;
using Financias.API.Domain.DTOs.Response;
using RabbitMQ.Client;
using IModel = RabbitMQ.Client.IModel;


namespace Financias.API.Infrastructure.RabbitMqClient;

public class RabbitMqClient : IRabbitMqClient
{
    private readonly IConfiguration _configuration;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMqClient(IConfiguration configuration)
    {
        _configuration = configuration;

        var factory = new ConnectionFactory()
        {
            HostName = _configuration["RabbitMqHost"],
            Port = int.Parse(_configuration["RabbitMqPort"])
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        
        _channel.ExchangeDeclare(exchange: "TransacaoExchange", type: ExchangeType.Fanout);
    }

    public void PublicaTransacaoCriada(TransacaoResponseRabbitMq transacaoCriada)
    {
        var message = JsonSerializer.Serialize(transacaoCriada);
        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(
            exchange: "TransacaoExchange",
            routingKey: "",
            basicProperties: null,
            body: body);
    }
}