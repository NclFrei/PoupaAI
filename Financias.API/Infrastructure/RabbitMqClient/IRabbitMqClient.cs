using Financias.API.Domain.DTOs.Response;

namespace Financias.API.Infrastructure.RabbitMqClient;

public interface IRabbitMqClient
{
    void PublicaTransacao(TransacaoResponseRabbitMq transacaoCriada);
}