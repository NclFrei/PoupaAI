using Financias.API.Domain.DTOs.Response;

namespace Financias.API.Infrastructure.RabbitMqClient;

public interface IRabbitMqClient
{
    void PublicaTransacaoCriada(TransacaoResponseRabbitMq transacaoCriada);
}