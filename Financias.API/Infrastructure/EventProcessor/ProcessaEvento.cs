using AutoMapper;
using Financias.API.Domain.DTOs.Request;
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

    public void Processa(string mensagem)
    {
        using var scope = _scopeFactory.CreateScope();

        var usuarioRepository = scope.ServiceProvider.GetRequiredService<IUsuarioRepository>();

        var usuarioReadDto = JsonSerializer.Deserialize<UsuarioRequest>(mensagem);

        var usuario = _mapper.Map<Usuario>(usuarioReadDto);

        if(!usuarioRepository.ExisteUsuario(usuario.Id))
        {
            usuarioRepository.CriaUsuario(usuario);
            usuarioRepository.SaveChanges();
        }
    }
       

}
