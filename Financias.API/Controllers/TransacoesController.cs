using Financias.API.Application.Services;
using Financias.API.Domain.DTOs.Request;
using Microsoft.AspNetCore.Mvc;

namespace Financias.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransacoesController : ControllerBase
{
    private readonly TransacaoService _transacaoService;

    public TransacoesController(TransacaoService transacaoService)
    {
        _transacaoService = transacaoService;
    }


}