using Financias.API.Domain.Enums;

namespace Financias.API.Domain.Models;

public class Transacao
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public DateTime DataTransacao { get; set; }
    public decimal Valor { get; set; }
    public int CategoriaId { get; set; }
    public TipoTransacao Tipo { get; set; }
    
    public Categoria Categoria { get; set; }
    public int UsuarioId { get; set; }
    
}