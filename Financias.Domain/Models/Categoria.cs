using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financias.Domain.Models;

public class Categoria
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Icone { get; set; } = string.Empty;
    public string CorHex { get; set; } = string.Empty;
}
