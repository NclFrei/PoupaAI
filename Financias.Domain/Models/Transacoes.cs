using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financias.Domain.Models;

public class Transacoes
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public DateTime DataTransacao { get; set; }
    public decimal Valor { get; set; }
    public int CategoriaId { get; set; } 
    public Categoria Categoria { get; set; } 
    public int UsuarioId { get; set; } 
}
