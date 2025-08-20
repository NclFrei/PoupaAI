using Chatbot.API.Application.Service;
using Chatbot.API.Domain.DTOs.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chatbot.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatbotController : ControllerBase
    {
        private readonly ChatbotService _chatbotService;

        public ChatbotController(ChatbotService chatbotService)
        {
            _chatbotService = chatbotService;
        }

        [HttpPost("Perguntar")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AnalyzeTransactions([FromBody] PerguntaRequest request)
        {
            if (request == null || request.UsuarioId <= 0 || string.IsNullOrEmpty(request.Pergunta))
            {
                return BadRequest("Requisição inválida. Certifique-se de fornecer o ID do usuário e a pergunta.");
            }

            var answer = await _chatbotService.GetAnswerForUserAsync(request.UsuarioId, request.Pergunta);

            if (answer.Contains("Não encontrei nenhuma transação"))
            {
                return NotFound(new { Answer = answer });
            }

            return Ok(new { Answer = answer });
        }
    }
}
