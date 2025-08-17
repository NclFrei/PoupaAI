using AutoMapper;
using Financias.API.Domain.DTOs.Request;
using Financias.API.Domain.Interfaces;
using Financias.API.Domain.Models;
using System.Text.Json;

namespace Financias.API.Infrastructure.EventProcessor;

public class ProcessaEvento : IProcessaEvento
{
   private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMapper _mapper ;

    public ProcessaEvento(IServiceScopeFactory scopeFactory, IMapper mapper)
    {
        _scopeFactory = scopeFactory;
        _mapper = mapper;
    }

    public async Task Processa(string mensagem)
    {
        using var scope = _scopeFactory.CreateScope();

        var usuarioRepository = scope.ServiceProvider.GetRequiredService<IUsuarioRepository>();

        var usuarioReadDto = JsonSerializer.Deserialize<UsuarioRequest>(mensagem);
        if (usuarioReadDto == null) return;

        var usuarioExistente = await usuarioRepository.BuscaUsuarioExterno(usuarioReadDto.Id);

        if (usuarioExistente == null)
        {
            var usuario = _mapper.Map<Usuario>(usuarioReadDto);
            await usuarioRepository.CreateUsuario(usuario);
        }
        else
        {
            _mapper.Map(usuarioReadDto, usuarioExistente); 
            await usuarioRepository.AtualizarAsync(usuarioExistente);
        }
    }
       

}
