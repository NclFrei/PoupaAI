using Financias.API.Domain.Enums;

namespace Financias.API.Domain.DTOs.Request;

public class AtualizaTransacaoRequest
{
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public DateTime? DataTransacao { get; set; }
    public decimal? Valor { get; set; }
    public int? CategoriaId { get; set; }
    public TipoTransacao Tipo { get; set; }
}