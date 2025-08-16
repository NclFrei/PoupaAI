using Chatbot.API.Domain.Interfaces;
using System.Text.Json;

namespace Chatbot.API.Application.Service;

public class ChatbotService
{
    private readonly ITransacaoRepository _transacaoRepository;
    private readonly GeminiAssistantService _geminiAssistantService;

    public ChatbotService(ITransacaoRepository transacaoRepository, GeminiAssistantService geminiAssistantService)
    {
        _transacaoRepository = transacaoRepository;
        _geminiAssistantService = geminiAssistantService;
    }

    public async Task<string> GetAnswerForUserAsync(int usuarioId, string userQuestion)
    {
        var transacoesRecentes = await _transacaoRepository.ObterTransacoesRecentesPorUsuarioAsync(usuarioId, 20);

        var transactionsJson = JsonSerializer.Serialize(transacoesRecentes);

        var resposta = await _geminiAssistantService.GetTransactionSummary(transactionsJson, userQuestion);

        return resposta;
    }
}