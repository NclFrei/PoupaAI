namespace Usuarios.API.Domain.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public DateTime DataCadastro { get; set; } = DateTime.Now;
    public DateTime DataNascimento { get; set; }

    public void SetPassword(string senha)
    {
        Senha = BCrypt.Net.BCrypt.HashPassword(senha);
    }

    public bool CheckPassword(string senha)
    {
        return BCrypt.Net.BCrypt.Verify(senha, Senha);
    }
}
