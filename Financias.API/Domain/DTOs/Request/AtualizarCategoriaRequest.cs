namespace Financias.API.Domain.DTOs.Request;

public class AtualizarCategoriaRequest
{
    public string? Nome { get; set; } = string.Empty;
    public string? Icone { get; set; } = string.Empty;
    public string? CorHex { get; set; } = string.Empty;
}