using System.Net;
using System.Net.Http.Headers;

namespace Financias.API.HttpClients;

public class UsuarioServiceHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public UsuarioServiceHttpClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<bool> VerificarUsuarioExisteAsync(int usuarioId, string jwtToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"api/Usuario/{usuarioId}");

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

        // Imprime o token
        Console.WriteLine($"Token enviado para a API de Usuário: {jwtToken}");

        // Imprime a URI final
        Console.WriteLine($"Enviando requisição para: {_httpClient.BaseAddress}{request.RequestUri}");

        // Imprime todos os headers
        Console.WriteLine("HEADERS DA REQUISIÇÃO:");
        foreach (var header in request.Headers)
        {
            Console.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
        }

        var response = await _httpClient.SendAsync(request);

        if (response.StatusCode == HttpStatusCode.NotFound)
            return false;

        response.EnsureSuccessStatusCode();
        return true;
    }
}