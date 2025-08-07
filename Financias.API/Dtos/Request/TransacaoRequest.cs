namespace Financias.API.Dtos.Request;


public class TransacaoRequest
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public DateTime DataTransacao { get; set; }
    public decimal Valor { get; set; }
    public int CategoriaId { get; set; }
    public int UsuarioId { get; set; }
}
