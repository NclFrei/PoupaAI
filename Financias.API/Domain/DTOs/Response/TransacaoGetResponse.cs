using Financias.API.Domain.Enums;
using Financias.API.Domain.Models;

namespace Financias.API.Domain.DTOs.Response;

public class TransacaoGetResponse
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public DateTime DataTransacao { get; set; }
    public decimal valor { get; set; }
    public int CategoriaId { get; set; }
    public TipoTransacao Tipo { get; set; }
    public int UsuarioId { get; set; }
    
    public CategoriaResponse Categoria { get; set; }
    public UsuarioResponse Usuario { get; set; }
}