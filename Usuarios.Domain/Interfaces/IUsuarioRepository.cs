using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Domain.Models;

namespace Usuarios.Domain.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> BuscarPorIdAsync(int id);
    Task<Usuario?> BuscarPorEmailAsync(string email);
    Task<bool> VerificaEmailExisteAsync(string email);
    Task<Usuario> CriarAsync(Usuario usuario);
    Task<bool> DeleteAsync(Usuario usuario);
    Task<Usuario> AtualizarAsync(Usuario usuario);
}
