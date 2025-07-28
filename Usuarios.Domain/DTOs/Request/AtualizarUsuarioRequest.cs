using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usuarios.Domain.DTOs.Request;

public class AtualizarUsuarioRequest
{
    public string? Nome { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public string? Senha { get; set; } = string.Empty;
    public DateTime? DataNascimento { get; set; }
}
