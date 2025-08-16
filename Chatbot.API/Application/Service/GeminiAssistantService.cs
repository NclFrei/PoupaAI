namespace Chatbot.API.Application.Service;
using Mscc.GenerativeAI;

public class GeminiAssistantService
{
    private readonly GenerativeModel _model;

    public GeminiAssistantService(IConfiguration configuration)
    {
        var apiKey = configuration["GoogleAiApiKey"]
                     ?? throw new InvalidOperationException("Gemini API key não configurada!");

        var googleAI = new GoogleAI(apiKey: apiKey);
        _model = googleAI.GenerativeModel(model: Model.Gemini20FlashLite);
    }

    public async Task<string> GetTransactionSummary(string transactionsJson, string userQuestion)
    {
        var prompt = $@"
            Você é um assistente financeiro pessoal. Analise as transações do usuário e responda em texto contínuo e amigável.
            Não use \n ou símbolos de markdown. Use apenas parágrafos separados por espaços.

            Transações:
            {transactionsJson}

            Pergunta do usuário:
            {userQuestion}

            Responda de forma clara e concisa:";


        var response = await _model.GenerateContent(prompt);
        return response.Text;
    }
}
