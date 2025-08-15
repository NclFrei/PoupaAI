namespace Financias.API.Domain.DTOs.Response;

public class TransacaoResponseRabbitMq
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public DateTime DataTransacao { get; set; }
    public decimal Valor { get; set; }
    public string Categoria { get; set; }
    public string Tipo { get; set; }
}